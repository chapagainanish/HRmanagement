using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IAttendenceRepository, AttendenceRepository>();
            services.AddScoped<IPayrollRepository, PayrollRepository>();
            services.AddScoped<ITravelExpenseRepository, TravelExpenseRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IJwtService, JwtService>();

            return services;
        }
    }
}
