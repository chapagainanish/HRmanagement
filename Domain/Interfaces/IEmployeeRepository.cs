using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee?> GetByCodeAsync(string code, bool includeRelated = false, CancellationToken cancellationToken = default);
        Task<Employee?> GetByEmailAsync(string email, bool includeRelated = false, CancellationToken cancellationToken = default);
        Task<IEnumerable<Employee>> GetByOrganizationAsync(int organizationId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Attendence>> GetAttendancesAsync(int employeeId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
    }
}