using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAttendenceService
    {
        Task<IEnumerable<AttendenceDto>> GetByEmployeeAsync(int employeeId, DateTime? from = null, DateTime? to = null);
        Task<IEnumerable<AttendenceDto>> GetByDateAsync(DateTime date);
        Task<AttendenceDto> CreateAsync(CreateAttendenceDto dto);
        Task<bool> UpdateAsync(int attendanceId, UpdateAttendenceDto dto);
        Task<bool> DeleteAsync(int attendanceId);
    }
}