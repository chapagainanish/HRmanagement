using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;

namespace Application.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CreateOrganizationDto> _createValidators;
        readonly IValidator<UpdateOrganizationDto> _updateValidators;
        public OrganizationService(IUnitOfWork unitOfWork,
            IValidator<CreateOrganizationDto> createValidators,
            IValidator<UpdateOrganizationDto> updateValidators
            )
        {
            _unitOfWork = unitOfWork;
            _createValidators = createValidators;
            _updateValidators = updateValidators;

        }

        public async Task<IEnumerable<OrganizationDto>> GetAllAsync()
        {
            var items = await _unitOfWork.Organizations.GetAllWithEmployeeCountAsync();

            var result = new List<OrganizationDto>();
            foreach (var item in items)
            {
                result.Add(MapToDto(item));
            }
            return result;
        }

        public async Task<OrganizationDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.Organizations.GetWithEmployeesAsync(id);
            if (entity == null)
            {
                return null;
            }
            return MapToDto(entity);
        }

        public async Task<OrganizationDto> CreateAsync(CreateOrganizationDto dto)
        {
            var validationResult = await _createValidators.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entity = new Organization
            {
                OrganizationName = dto.OrganizationName,
                Address = dto.Address ?? string.Empty,
                ContactEmail = dto.ContactEmail ?? string.Empty,
                ContactPhone = dto.ContactPhone ?? string.Empty


            };

            await _unitOfWork.Organizations.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateOrganizationDto dto)
        {
            var validationResult = await _updateValidators.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var entity = await _unitOfWork.Organizations.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(dto.OrganizationName))
            {
                entity.OrganizationName = dto.OrganizationName;
            }

            _unitOfWork.Organizations.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.Organizations.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _unitOfWork.Organizations.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private OrganizationDto MapToDto(Organization entity)
        {
            var employeeCount = 0;
            if (entity.Employees != null)
            {
                employeeCount = entity.Employees.Count;
            }

            return new OrganizationDto
            {
                OrganizationId = entity.OrganizationId,
                OrganizationName = entity.OrganizationName,
                EmployeeCount = employeeCount
            };
        }
    }
}
