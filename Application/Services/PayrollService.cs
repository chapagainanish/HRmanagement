using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    /// <summary>
    /// Service for managing payroll records
    /// </summary>
    public class PayrollService : IPayrollService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PayrollService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all payroll records for a specific month
        /// </summary>
        public async Task<IEnumerable<PayrollDto>> GetByMonthAsync(DateTime month)
        {
            // Get records from database
            var items = await _unitOfWork.Payrolls.GetByMonthAsync(month);

            // Convert to DTOs
            var result = new List<PayrollDto>();
            foreach (var item in items)
            {
                result.Add(MapToDto(item));
            }
            return result;
        }

        /// <summary>
        /// Get payroll record by ID
        /// </summary>
        public async Task<PayrollDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Payrolls.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            return MapToDto(entity);
        }

        /// <summary>
        /// Create a new payroll record
        /// </summary>
        public async Task<PayrollDto> CreateAsync(CreatePayrollDto dto)
        {
            var netSalary = dto.BasicSalary + dto.Allowance - dto.Deduction;

            // Create entity from DTO
            var entity = new Payroll
            {
                EmployeeId = dto.EmployeeId,
                SalaryMonth = dto.SalaryMonth,
                BasicSalary = dto.BasicSalary,
                Allowance = dto.Allowance,
                Deduction = dto.Deduction,
                NetSalary = netSalary
            };

            // Save to database
            await _unitOfWork.Payrolls.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(entity);
        }

        /// <summary>
        /// Update an existing payroll record
        /// </summary>
        public async Task<bool> UpdateAsync(int id, UpdatePayrollDto dto)
        {
            // Find existing record
            var entity = await _unitOfWork.Payrolls.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            // Update fields if provided
            if (dto.BasicSalary.HasValue)
            {
                entity.BasicSalary = dto.BasicSalary.Value;
            }
            if (dto.Allowance.HasValue)
            {
                entity.Allowance = dto.Allowance.Value;
            }
            if (dto.Deduction.HasValue)
            {
                entity.Deduction = dto.Deduction.Value;
            }

            // Recalculate net salary
            entity.NetSalary = entity.BasicSalary + entity.Allowance - entity.Deduction;

            // Save changes
            _unitOfWork.Payrolls.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Delete a payroll record
        /// </summary>
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Payrolls.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _unitOfWork.Payrolls.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Convert entity to DTO
        /// </summary>
        private PayrollDto MapToDto(Payroll entity)
        {
            return new PayrollDto
            {
                PayrollId = entity.PayrollId,
                EmployeeId = entity.EmployeeId,
                SalaryMonth = entity.SalaryMonth,
                BasicSalary = entity.BasicSalary,
                Allowance = entity.Allowance,
                Deduction = entity.Deduction,
                NetSalary = entity.NetSalary
            };
        }
    }
}