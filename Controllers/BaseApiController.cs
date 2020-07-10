using Microsoft.AspNetCore.Mvc;
using UniversityDemo.Identity;

namespace UniversityDemo.Controllers
{
    public class BaseApiController : ControllerBase
    {
        protected IUserInfo UserInfo { get; }
    }
}