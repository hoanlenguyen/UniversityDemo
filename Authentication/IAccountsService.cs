using Microsoft.AspNetCore.Mvc;
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