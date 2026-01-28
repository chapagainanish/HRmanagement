using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IOrganizationRepository Organizations { get; }
        IAttendenceRepository Attendences { get; }
        IPayrollRepository Payrolls { get; }
        ITravelExpenseRepository TravelExpences { get; }
        IUserRepository Users { get; }
        IRefreshTokenRepository RefreshToken { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
