using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using UniversityDemo.Authentication;
using UniversityDemo.Permissions;
using UniversityDemo.Repositories;
using UniversityDemo.Repositories.Internal;
using UniversityDemo.Services;

namespace UniversityDemo.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountsService, AccountsService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<StudentService>();

            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<PostService>();

            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<BlogService>();

            services.AddScoped<RoleService>();

            services.AddScoped<FileService>();
            return services;
        }

        public static IServiceCollection AddPermissionServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            return services;
        }

        public static IServiceCollection AddAuthorizations(this IServiceCollection services)
        {
            services.AddPermissionServices();
            services.AddAuthorization();
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(RequiredPermissions.Blogs.View, builder =>
            //    {
            //        builder.AddRequirements(new PermissionRequirement(RequiredPermissions.Blogs.View));
            //    });
            //});

            return services;
        }
    }
}