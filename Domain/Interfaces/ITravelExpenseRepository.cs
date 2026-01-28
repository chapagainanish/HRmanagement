using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITravelExpenseRepository : IRepository<TravelExpense>
    {
        Task<IEnumerable<TravelExpense>> GetByEmployeeAsync(int employeeId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<TravelExpense>> GetByStatusAsync(string status, CancellationToken cancellationToken = default);
    }
}
