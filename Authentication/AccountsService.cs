using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniversityDemo.Authorization;
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

        public async Task<JsonResult> Register([FromForm] RegisterCredentials input)
        {
            var user = new ApplicationUser(input.UserName, input.Email);
            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: true);
                var createdUser = await _userManager.FindByNameAsync(input.UserName);
                return new JsonResult(new Dictionary<string, object>
                    {
                        { "userId", createdUser.Id },
                        { "userName", createdUser.UserName },
                        { "email", createdUser.Email },
                        { "accessToken", GenerateJSONWebToken(createdUser) },
                    });
            }
            return new JsonResult(false);
        }

        public async Task<JsonResult> Login([FromForm] LoginCredentials input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(input.UserName);
                var userInfo = user.ToUserInfo();
                var roles = await _userManager.GetRolesAsync(user);
                userInfo.Roles = new List<string>(roles);
                return new JsonResult(new Dictionary<string, object>
                    {
                        { "userId", userInfo.Id },
                        { "userName", userInfo.UserName },
                        { "email", userInfo.Email },
                        { "roles", roles },
                        { "accessToken", $"Bearer {GenerateJSONWebToken(user)}" },
                    });
            }
            return new JsonResult("Unable to sign in") { StatusCode = 401 };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new Exception("Role name must not be null or empty");

            bool checkExist = await _roleManager.RoleExistsAsync(RoleNames.Admin);
            if (checkExist)
                throw new Exception("Role name is already existed!");

            var role = new ApplicationRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task<bool> AddRolesToUser(string userId, bool createRoleIfNotExist = true, params string[] roleNames)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                var createdRoles = new List<string>();
                if (user != null)
                {
                    foreach (var roleName in roleNames)
                    {
                        bool checkExist = await _roleManager.RoleExistsAsync(roleName);
                        if (!checkExist && createRoleIfNotExist)
                        {
                            var role = new ApplicationRole(roleName);
                            var result = await _roleManager.CreateAsync(role);
                            if (result.Succeeded)
                                createdRoles.Add(roleName);
                        }
                        else if (checkExist)
                        {
                            createdRoles.Add(roleName);
                        }
                    }
                    await _userManager.AddToRolesAsync(user, createdRoles);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return true;
        }

        public async Task<List<string>> GetUserRolesByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = new List<string>(_userManager.GetRolesAsync(user).GetAwaiter().GetResult());
            return roles;
        }

        private string GenerateJSONWebToken(ApplicationUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = new List<string>(_userManager.GetRolesAsync(user).GetAwaiter().GetResult());

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.AuthTime,DateTime.UtcNow.ToUniversalTime().ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            claims.AddRange(roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role)));
            //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            //claimsIdentity.AddClaims(userInfo.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
              _options.Issuer,
              _options.Audience,
              claims,
              expires: DateTime.UtcNow.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}