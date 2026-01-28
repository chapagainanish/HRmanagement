using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IPayrollService
    {
        Task<IEnumerable<PayrollDto>> GetByMonthAsync(DateTime month);
        Task<PayrollDto?> GetByIdAsync(int id);
        Task<PayrollDto> CreateAsync(CreatePayrollDto dto);
        Task<bool> UpdateAsync(int id, UpdatePayrollDto dto);
        Task<bool> DeleteAsync(int id);
    }
}