using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityDemo.Authentication
{
    public interface IAccountsService
    {
        Task<JsonResult> Register(Credentials input);
        Task<JsonResult> Login(Credentials input);
        Task Logout();
    }
}
