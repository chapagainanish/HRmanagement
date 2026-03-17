using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DependencyInjection;
using Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace MgmtAPI.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {
            services.AddApplicationDI();

            services.AddInfrastructureDI();

            return services;
        }
    }
}
