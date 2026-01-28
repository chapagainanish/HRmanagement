using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;

namespace Infrastructure.Repository
{
    public class TravelExpenseRepository : Repository<TravelExpense>, ITravelExpenseRepository
    {
        public TravelExpenseRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TravelExpense>> GetByEmployeeAsync(int employeeId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
        {
            var q = _dbSet.AsNoTracking().Where(t => t.EmployeeId == employeeId);

            if (from.HasValue) q = q.Where(t => t.TravelDate >= from.Value);
            if (to.HasValue) q = q.Where(t => t.TravelDate <= to.Value);

            return await q.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TravelExpense>> GetByStatusAsync(string status, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().Where(t => t.Status == status).ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
