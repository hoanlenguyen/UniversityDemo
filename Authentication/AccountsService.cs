using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        public AccountsService(
            UserManager<ApplicationUser> userManager
            , RoleManager<ApplicationRole> roleManager
            , SignInManager<ApplicationUser> signInManager
            , IOptions<JWTSettings> optionsAccessor
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _options = optionsAccessor.Value;
        }

        public async Task<JsonResult> Register([FromForm]Credentials input)
        {
            var user = new ApplicationUser { UserName = input.Email, Email = input.Email };
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                var userInfo = (await _userManager.FindByEmailAsync(input.Email)).ToUserInfo();
                return new JsonResult(new Dictionary<string, object>
                    {
                        { "userId", userInfo.Id },
                        { "userName", userInfo.UserName },
                        { "email", userInfo.Email },
                        { "accessToken", GenerateJSONWebToken(userInfo) },
                    });
            }
            return new JsonResult(false);
        }

        public async Task<JsonResult> Login(Credentials input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, false);
            if (result.Succeeded)
            {
                var userInfo = (await _userManager.FindByEmailAsync(input.Email)).ToUserInfo();
                //need logout??
                return new JsonResult(new Dictionary<string, object>
                    {
                        { "userId", userInfo.Id },
                        { "userName", userInfo.UserName },
                        { "email", userInfo.Email },
                        { "accessToken", GenerateJSONWebToken(userInfo) },
                    });
            }
            return new JsonResult("Unable to sign in") { StatusCode = 401 };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        private string GenerateJSONWebToken(UserInfo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                new Claim(JwtRegisteredClaimNames.AuthTime,DateTime.UtcNow.ToString("yyyy-MM-dd-HH:mm:ss")),
                new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id)
            };
            var token = new JwtSecurityToken(
              _options.Issuer,
              _options.Audience,
              claims,
              expires: DateTime.UtcNow.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //public string GetSecretKey()
        //{
        //    return _options.SecretKey;
        //}
    }
}