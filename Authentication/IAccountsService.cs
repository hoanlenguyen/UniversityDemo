using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UniversityDemo.Authentication
{
    public interface IAccountsService
    {
        Task<JsonResult> Register(RegisterCredentials input);

        Task<JsonResult> Login(LoginCredentials input);

        Task Logout();

        //string GetSecretKey();

        //Task CreateAdminUserAndRole();

        Task<bool> AddRolesToUser(string userId, bool createRoleIfNotExists = true, params string[] roleNames);
    }
}