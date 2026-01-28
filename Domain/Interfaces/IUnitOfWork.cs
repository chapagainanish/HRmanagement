using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IEmployeeRepository Employees { get; }
        IOrganizationRepository Organizations { get; }
        IAttendenceRepository Attendences { get; }
        IPayrollRepository Payrolls { get; }
        ITravelExpenseRepository TravelExpences { get; }
        IUserRepository Users { get; }

        Task<int> SaveChangesAsync();
    }
}
