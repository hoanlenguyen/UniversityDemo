using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityDemo.Test;

namespace UniversityDemo.Extensions
{
    public static class TestInjectionServiceExtensions
    {
        public static IServiceCollection AddTestInjectionServices(this IServiceCollection services)
        {
            //test Injection dependency
            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped, Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.NewGuid()));
            services.AddTransient<OperationService, OperationService>();
            return services;
        }
    }
}