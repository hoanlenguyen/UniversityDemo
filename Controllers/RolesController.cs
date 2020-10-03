using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UniversityDemo.Authentication;
using UniversityDemo.Controllers.BaseControllers;
using UniversityDemo.Services;

namespace UniversityDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : BaseApiController
    {
        private readonly IAccountsService _accountsService;

        private readonly RoleService _roleService;

        public RolesController(IAccountsService accountsService, RoleService roleService)
        {
            _accountsService = accountsService;
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string roleName)
        {
            return Ok(await _accountsService.CreateRoleAsync(roleName));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _roleService.GetAllAsync());
        }

        [HttpPost("{roleName}/Assign/Permissions")]
        public async Task<IActionResult> AssignPermissionsToRole(string roleName, params string[] permissions)
        {
            return Ok(await _roleService.AssignPermissionsToRoleAsync(roleName, permissions));
        }
    }
}