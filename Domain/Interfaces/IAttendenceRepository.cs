using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAttendenceRepository : IRepository<Attendence>
    {
        Task<IEnumerable<Attendence>> GetByEmployeeAsync(int employeeId, DateTime? from = null, DateTime? to = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<Attendence>> GetByDateAsync(DateTime date, CancellationToken cancellationToken = default);
    }
}
