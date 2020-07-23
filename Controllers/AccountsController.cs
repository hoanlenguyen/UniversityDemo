using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityDemo.Authentication;
using UniversityDemo.Authorization;
using UniversityDemo.Controllers.BaseControllers;

namespace UniversityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : BaseApiController
    {
        private readonly IAccountsService _accountsService;

        public AccountsController(IAccountsService accountsService)
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

        [Authorize(Roles = RoleNames.Admin)]
        [HttpGet("current/userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            //var accessToken = HttpContext.Request.Headers["Authorization"];
            var user = base.GetUserInfo(User);
            return new JsonResult(
                    new Dictionary<string, object>
                    {
                        { "userId",user.Id },
                        { "userName", user.UserName},
                        { "email", user.Email }
                    });
        }

        [HttpPost("{userId}/Assign/Roles")]
        public async Task<IActionResult> AssignRolesToUser(string userId, bool createRoleIfNotExists = true, params string[] roleNames)
        {
            await _accountsService.AddRolesToUser(userId, createRoleIfNotExists, roleNames);
            return Ok();
        }

        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            return Ok(await _accountsService.GetUserRolesByUserId(userId));
        }

        private JsonResult Error(string message)
        {
            return new JsonResult(message) { StatusCode = 400 };
        }
    }
}