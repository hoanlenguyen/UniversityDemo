using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Threading.Tasks;
using UniversityDemo.Authorization;
using UniversityDemo.Data;
using UniversityDemo.Enum;
using UniversityDemo.Identity;
using UniversityDemo.Models.DTO;

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
                await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, permission));
            }

            //Type type = typeof(Permissions.BlogPermissions);
            //foreach (var p in type.GetProperties())
            //{
            //    var ojValue = p.GetValue(null, null);
            //    var value = (string)ojValue;
            //    await _roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, value));
            //}




            return true;
        }

        public async Task<List<string>> GetBlogPermissions()
        {
            var permissions = new List<string>();
            //Type type = typeof(BlogPermissions);
            //foreach (var p in type.GetProperties())
            //{
            //    var ojValue = p.GetValue(null, null);
            //    //var value = (string)ojValue;
            //    var value = GetStaticFieldValues(ojValue);
            //    permissions.Add(value);
            //}
            //permissions.Add(type.GetProperties().Length.ToString());
            return permissions;
        }

        private static Dictionary<string, string> GetFieldValues(object obj)
        {
            return obj.GetType()
                      .GetFields(BindingFlags.Public | BindingFlags.Static)
                      .Where(f => f.FieldType == typeof(string))
                      .ToDictionary(f => f.Name,
                                    f => (string)f.GetValue(null));
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
