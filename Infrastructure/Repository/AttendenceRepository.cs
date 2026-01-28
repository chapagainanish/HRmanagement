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
    public class AttendenceRepository : Repository<Attendence>, IAttendenceRepository
    {
        public AttendenceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Attendence>> GetByEmployeeAsync(int employeeId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default)
        {
            var q = _dbSet.AsNoTracking().Where(a => a.EmployeeId == employeeId);

            if (from.HasValue) q = q.Where(a => a.Date >= from.Value);
            if (to.HasValue) q = q.Where(a => a.Date <= to.Value);

            return await q.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Attendence>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default)
        {
            return await _dbSet.AsNoTracking().Where(a => a.Date.Date == date.Date).ToListAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
