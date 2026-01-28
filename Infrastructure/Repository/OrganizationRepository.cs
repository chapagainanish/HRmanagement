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
    public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
    {
        public OrganizationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Organization>> GetAllWithEmployeeCountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(o => o.Employees)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<Organization?> GetWithEmployeesAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(o => o.Employees)
                .FirstOrDefaultAsync(o => o.OrganizationId == id, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
