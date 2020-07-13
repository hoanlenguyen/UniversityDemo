using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityDemo.Authentication;
using UniversityDemo.Identity;

namespace UniversityDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountsService _accountsService;
        public AccountController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]Credentials credentials)
        {
            if (ModelState.IsValid)
            {
                return await _accountsService.Register(credentials);
            }
            return Error("Unexpected error");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]Credentials credentials)
        {
            if (ModelState.IsValid)
            {
                return await _accountsService.Login(credentials);
            }
            return Error("Unexpected error");
        }

        [Authorize]
        [HttpGet("current/userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            return new JsonResult(
                    new Dictionary<string, object>
                    {
                        { "isAuthenticated",User.Identity.IsAuthenticated },
                        { "userId",User.FindFirstValue(ClaimTypes.NameIdentifier) },
                        { "userName", User.Identity.Name},
                    });
        }

        [HttpGet]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _accountsService.Logout();
            return Ok();
        }

        private JsonResult Error(string message)
        {
            return new JsonResult(message) { StatusCode = 400 };
        }

    }
}