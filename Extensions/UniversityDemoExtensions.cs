using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityDemo.Extensions
{
    public static class UniversityDemoExtensions
    {
        public static IServiceCollection AddNearmeData(this IServiceCollection services)
        {
            return AddDataCosmos(services);
        }

        private static IServiceCollection AddDataCosmos(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}
