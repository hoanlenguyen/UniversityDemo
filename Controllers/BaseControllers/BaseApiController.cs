using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniversityDemo.Identity;

namespace UniversityDemo.Controllers.BaseControllers
{
    public class BaseApiController : ControllerBase
    {
        //protected IUserInfo UserInfo { get; }
        protected UserInfo GetUserInfo(ClaimsPrincipal user)
        {
            return user != null ?
                new UserInfo
                {
                    Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    UserName = User.Identity.Name
                }
                : new UserInfo();
        }
    }
}