using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversityDemo.Identity;

namespace UniversityDemo.Controllers.BaseControllers
{
    public class BaseApiController : ControllerBase
    {
        protected IUserInfo UserInfo => GetUserInfo(User);

        private UserInfo GetUserInfo(ClaimsPrincipal claims)
        {
            return claims != null ?
                new UserInfo
                {
                    Id = claims.FindFirstValue(ClaimTypes.NameIdentifier),
                    UserName = claims.Identity.Name ?? claims.FindFirstValue(ClaimTypes.Name),
                    Email = claims.FindFirstValue(ClaimTypes.Email)
                }
                : new UserInfo();
        }
    }
}