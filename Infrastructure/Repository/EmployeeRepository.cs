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
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Employee?> GetByCodeAsync(string code, bool includeRelated = false, CancellationToken cancellationToken = default)
        {
            var q = _dbSet.AsQueryable();
            if (includeRelated)
            {
                q = q.Include(e => e.Organization)
                     .Include(e => e.Attendances)
                     .Include(e => e.Payrolls)
                     .Include(e => e.Performances)
                     .Include(e => e.Recruitments)
                     .Include(e => e.TravelExpences);
            }

            return await q.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeCode == code, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Employee?> GetByEmailAsync(string email, bool includeRelated = false, CancellationToken cancellationToken = default)
        {
            var q = _dbSet.AsQueryable();
            if (includeRelated)
            {
                q = q.Include(e => e.Organization)
                     .Include(e => e.Attendances)
                     .Include(e => e.Payrolls)
                     .Include(e => e.Performances)
                     .Include(e => e.Recruitments)
                     .Include(e => e.TravelExpences);
            }

            return await q.AsNoTracking().FirstOrDefaultAsync(e => e.Email == email, cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Employee>> GetByOrganizationAsync(int organizationId, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking()
                .Where(e => e.OrganizationId == organizationId)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<Attendence>> GetAttendancesAsync(int employeeId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
        {
            var q = _context.Attendences.AsQueryable().Where(a => a.EmployeeId == employeeId);

            if (from.HasValue) q = q.Where(a => a.Date >= from.Value);
            if (to.HasValue) q = q.Where(a => a.Date <= to.Value);

            return await q.AsNoTracking().ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
