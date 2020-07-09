using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityDemo.Identity;

namespace UniversityDemo.Authentication
{
    //dotnet add package JWT -v 3.0.0-beta4
    //dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
    //dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
    public class AccountsService : IAccountsService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWTSettings _options;
        private readonly ILogger<AccountsService> _logger;

        public AccountsService(
            UserManager<ApplicationUser> userManager
            , RoleManager<ApplicationRole> roleManager
            , SignInManager<ApplicationUser> signInManager
            , IOptions<JWTSettings> optionsAccessor
            , ILogger<AccountsService> logger
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _options = optionsAccessor.Value;
            _logger = logger;
        }

        public async Task<JsonResult> Register([FromForm]Credentials input)
        {
            var user = new ApplicationUser { UserName = input.Email, Email = input.Email };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return new JsonResult(new Dictionary<string, object>
                {
                    { "access_token", GetAccessToken(input.Email) },
                    { "id_token", GetIdToken(user) }
                });
            }
            return new JsonResult(false);
        }

        public async Task<JsonResult> Login(Credentials input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(input.Email);
                //_logger.LogInformation("User {0} has logged", user.UserName);
                return new JsonResult(new Dictionary<string, object>
                    {
                        { "access_token", GetAccessToken(input.Email) },
                        { "id_token", GetIdToken(user) }
                    });
            }
            return new JsonResult("Unable to sign in") { StatusCode = 401 };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private string GetAccessToken(string Email)
        {
            var payload = new Dictionary<string, object>
              {
                { "sub", Email },
                { "email", Email }
              };
            return GetToken(payload);
        }

        private string GetToken(Dictionary<string, object> payload)
        {
            var secret = _options.SecretKey;

            payload.Add("iss", _options.Issuer);
            payload.Add("aud", _options.Audience);
            payload.Add("nbf", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("iat", ConvertToUnixTimestamp(DateTime.Now));
            payload.Add("exp", ConvertToUnixTimestamp(DateTime.Now.AddDays(7)));
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(payload, secret);
        }

        private string GetIdToken(IdentityUser user)
        {
            var payload = new Dictionary<string, object>
          {
            { "id", user.Id },
            { "sub", user.Email },
            { "email", user.Email },
            { "emailConfirmed", user.EmailConfirmed },
          };
            return GetToken(payload);
        }

        private static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }
    }
}
