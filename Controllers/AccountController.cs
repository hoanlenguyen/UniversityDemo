﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Authentication;
using UniversityDemo.Controllers.BaseControllers;

namespace UniversityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountsService _accountsService;

        public AccountController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterCredentials credentials)
        {
            if (ModelState.IsValid)
            {
                return await _accountsService.Register(credentials);
            }
            return Error("Unexpected error");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginCredentials credentials)
        {
            if (ModelState.IsValid)
            {
                return await _accountsService.Login(credentials);
            }
            return Error("Unexpected error");
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountsService.Logout();
            return Ok();
        }

        [Authorize]
        //[Authorize]
        [HttpGet("current/userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var accessToken = HttpContext.Request.Headers["Authorization"];
            var user = base.GetUserInfo(User);
            return new JsonResult(
                    new Dictionary<string, object>
                    {
                        { "userId",user.Id },
                        { "userName", user.UserName},
                        { "email", user.Email }
                    });
        }

        [HttpPost("{userName}/AssignRoles")]
        public async Task<IActionResult> AssignRolesToUser(string userName, bool createRoleIfNotExists = true, params string[] roleNames)
        {
            await _accountsService.AddRolesToUser(userName, createRoleIfNotExists, roleNames);
            return Ok();
        }

        private JsonResult Error(string message)
        {
            return new JsonResult(message) { StatusCode = 400 };
        }
    }
}