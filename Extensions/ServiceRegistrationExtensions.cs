using Microsoft.Extensions.DependencyInjection;
using UniversityDemo.Authentication;
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
            return services;
        }
    }
}