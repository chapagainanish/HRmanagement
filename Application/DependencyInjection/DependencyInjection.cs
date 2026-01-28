using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
namespace Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddScoped<IUserServices, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAttendenceService, AttendenceService>();
            services.AddScoped<IPayrollService, PayrollService>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<ITravelExpenseService, TravelExpenseService>();
            services.AddScoped<IAuthService, AuthService>();
            //takes all the validators from this assembly
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            //If individual validadators are required
            //// Organization validators
            //services.AddScoped<IValidator<CreateOrganizationDto>, CreateOrganizationDtoValidator>();
            //services.AddScoped<IValidator<UpdateOrganizationDto>, UpdateOrganizationDtoValidator>();

            //// Employee validators
            //services.AddScoped<IValidator<CreateEmployeeDto>, CreateEmployeeDtoValidator>();
            //services.AddScoped<IValidator<UpdateEmployeeDto>, UpdateEmployeeDtoValidator>();

            //// User validators
            //services.AddScoped<IValidator<CreateUserDto>, CreateUserDtoValidator>();
            //services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();

            //// Attendance validators
            //services.AddScoped<IValidator<CreateAttendenceDto>, CreateAttendenceDtoValidator>();
            //services.AddScoped<IValidator<UpdateAttendenceDto>, UpdateAttendenceDtoValidator>();

            //// Payroll validators
            //services.AddScoped<IValidator<CreatePayrollDto>, CreatePayrollDtoValidator>();
            //services.AddScoped<IValidator<UpdatePayrollDto>, UpdatePayrollDtoValidator>();

            //// Travel Expense validators
            //services.AddScoped<IValidator<CreateTravelExpenseDto>, CreateTravelExpenseDtoValidator>();
            //services.AddScoped<IValidator<UpdateTravelExpenseDto>, UpdateTravelExpenseDtoValidator>();

            //Auth Validators
            services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
            services.AddScoped<IValidator<RefreshTokenDto>, RefreshTokenDtoValidator>();

            return services;
        }
    }
}
