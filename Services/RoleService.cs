using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UniversityDemo.Authorization;
using UniversityDemo.Data;
using UniversityDemo.Enum;
using UniversityDemo.Identity;
using UniversityDemo.Models.DTO;
using UniversityDemo.Permissions;

namespace UniversityDemo.Services
{
    public class RoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        DemoDbContext Context { get; }
        IMapper Mapper { get; }
        DbSet<ApplicationRole> DbSet => Context.Roles;
        public RoleService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, DemoDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            Context = context;
            Mapper = mapper;
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
                throw new Exception("Role name must not be null or empty");

            bool checkExist = await _roleManager.RoleExistsAsync(RoleNames.Admin);
            if (checkExist)
                throw new Exception("Role name is already existed!");

            var role = new ApplicationRole(roleName);
            var result = await _roleManager.CreateAsync(role);
            return result.Succeeded;
        }

        public async Task<List<RoleDTO>> GetAllAsync()
        {
            return await DbSet.AsNoTracking().ProjectTo<RoleDTO>(Mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> AssignPermissionsToRoleAsync(string roleName, params string[] permissions)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return false;

            foreach (var permission in permissions)
            {
                var roleClaim = await Context.RoleClaims.FirstOrDefaultAsync(q => q.RoleId == role.Id && q.ClaimValue.ToLower() == permission.ToLower());
                if (roleClaim != null)
                    continue;

                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
            }

            return true;
        }

        public async Task<bool> UpdatePermissionsForRoleAsync(string roleName, params string[] permissions)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
                return false;

            var roleClaims = Context.RoleClaims.AsNoTracking().Where(x => x.RoleId == role.Id);
            Context.RoleClaims.RemoveRange(roleClaims);
            await Context.SaveChangesAsync();

            foreach (var permission in permissions)
            {
                //var roleClaim = await Context.RoleClaims.FirstOrDefaultAsync(q => q.RoleId == role.Id && q.ClaimValue.ToLower() == permission.ToLower());
                //if (roleClaim != null)
                //    continue;
                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
            }

            return true;
        }

        public async Task<List<IGrouping<string,string>>> GetAllPermissions()
        {
            var permissions = new List<string>();
            var groups = new List<IGrouping<string, string>>();
            foreach (var type in typeof(RequiredPermissions).GetNestedTypes(BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (!type.IsAbstract)
                    continue;

                //var className =type.Name;
                foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                {
                    //var fieldName = field.Name;
                    permissions.Add(field.GetValue(null).ToString());
;                }
            }

            var list = permissions.GroupBy(x => x.Split('.')[1]).ToList();
            
            return list;
        }

        private static string GetStaticFieldValues(object obj)
        {
            return obj.GetType()
                      .GetFields(BindingFlags.Public | BindingFlags.Static)
                      .Where(f => f.FieldType == typeof(string))
                      .Select(f => (string)f.GetValue(null))
                      .FirstOrDefault();
        }

    }
}
