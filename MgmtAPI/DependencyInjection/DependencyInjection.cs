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
    /// <summary>
    /// Extension methods for registering all application dependencies
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Register all services (Application + Infrastructure layers)
        /// </summary>
        public static IServiceCollection AddAppDependencies(this IServiceCollection services)
        {
            // Register Application layer services
            services.AddApplicationDI();

            // Register Infrastructure layer services (repositories, DbContext)
            services.AddInfrastructureDI();

            return services;
        }
    }
}
