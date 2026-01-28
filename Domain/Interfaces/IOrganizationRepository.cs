using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOrganizationRepository : IRepository<Organization>
    {
        Task<Organization?> GetWithEmployeesAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Organization>> GetAllWithEmployeeCountAsync(CancellationToken cancellationToken = default);
    }
}
