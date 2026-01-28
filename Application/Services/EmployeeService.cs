using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Employees.GetAllAsync();

            var result = new List<EmployeeDto>();
            foreach (var item in items)
            {
                result.Add(MapToDto(item));
            }
            return result;
        }

        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Employees.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            return MapToDto(entity);
        }

        public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto)
        {
            var entity = new Employee
            {
                FullName = dto.FullName,
                EmployeeCode = dto.EmployeeCode ?? string.Empty,
                Email = dto.Email,
                OrganizationId = dto.OrganizationId ?? 0,
                Position = dto.Position ?? string.Empty,
                HireDate = dto.HireDate ?? DateTime.UtcNow,
                Salary = dto.Salary ?? 0m
            };

            await _unitOfWork.Employees.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateEmployeeDto dto)
        {
            var entity = await _unitOfWork.Employees.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(dto.FullName))
            {
                entity.FullName = dto.FullName;
            }
            if (!string.IsNullOrEmpty(dto.EmployeeCode))
            {
                entity.EmployeeCode = dto.EmployeeCode;
            }
            if (!string.IsNullOrEmpty(dto.Email))
            {
                entity.Email = dto.Email;
            }
            if (dto.OrganizationId.HasValue)
            {
                entity.OrganizationId = dto.OrganizationId.Value;
            }
            if (!string.IsNullOrEmpty(dto.Position))
            {
                entity.Position = dto.Position;
            }
            if (dto.HireDate.HasValue)
            {
                entity.HireDate = dto.HireDate.Value;
            }
            if (dto.Salary.HasValue)
            {
                entity.Salary = dto.Salary.Value;
            }

            _unitOfWork.Employees.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Employees.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _unitOfWork.Employees.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private EmployeeDto MapToDto(Employee entity)
        {
            return new EmployeeDto
            {
                EmployeeId = entity.EmployeeId,
                EmployeeCode = entity.EmployeeCode,
                Email = entity.Email,
                OrganizationId = entity.OrganizationId,
                FullName = entity.FullName
            };
        }
    }
}
