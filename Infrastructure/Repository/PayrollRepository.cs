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
    public class PayrollRepository : Repository<Payroll>, IPayrollRepository
    {
        public PayrollRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Payroll?> GetByEmployeeAndMonthAsync(int employeeId, DateTime month, CancellationToken cancellationToken = default)
        {
            var start = new DateTime(month.Year, month.Month, 1);
            return await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(p => p.EmployeeId == employeeId && p.SalaryMonth.Year == start.Year && p.SalaryMonth.Month == start.Month, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Payroll>> GetByMonthAsync(DateTime month, CancellationToken cancellationToken = default)
        {
            var start = new DateTime(month.Year, month.Month, 1);
            return await _dbSet.AsNoTracking()
                .Where(p => p.SalaryMonth.Year == start.Year && p.SalaryMonth.Month == start.Month)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
