using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniversityDemo.Authentication
{
    public interface IAccountsService
    {
        Task<JsonResult> Register(RegisterCredentials input);

        Task<JsonResult> Login(LoginCredentials input);

        Task Logout();

        Task<bool> ChangePassword(string userName, string password);

        Task<bool> CreateRoleAsync(string roleName);

        Task<List<string>> GetUserRolesByUserId(string userId);

        Task<bool> AddRolesToUser(string userId, bool createRoleIfNotExists = true, params string[] roleNames);
    }
}