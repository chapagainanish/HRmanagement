using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPayrollRepository : IRepository<Payroll>
    {
        Task<Payroll?> GetByEmployeeAndMonthAsync(int employeeId, DateTime month, CancellationToken cancellationToken = default);
        Task<IEnumerable<Payroll>> GetByMonthAsync(DateTime month, CancellationToken cancellationToken = default);
    }
}
