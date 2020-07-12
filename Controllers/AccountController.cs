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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ClaimsPrincipal _caller;
        public AccountController(IAccountsService accountsService,
                                 UserManager<ApplicationUser> userManager,
                                 ClaimsPrincipal caller)
        {
            _accountsService = accountsService;
            _userManager = userManager;
            _caller = caller;
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

        [HttpGet]
        [Route("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            //var user = GetCurrentUserAsync().Result;
            //var userId = _userManager.GetUserId(HttpContext.User);
            string userName = User.Identity.IsAuthenticated? User.Identity.Name:null;
            var user = userName!=null? await _userManager.FindByIdAsync(userName) :null;
            //return new JsonResult(_caller.Claims.Select(
            //    c => new { c.Type, c.Value }));
            return new JsonResult(
                new Dictionary<string, object>
                    {
                        { "userName",user.Id },
                        { "userId", userName},
                    }
            );
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

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}