using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IOrganizationService
    {
        Task<IEnumerable<OrganizationDto>> GetAllAsync();
        Task<OrganizationDto?> GetByIdAsync(int id);
        Task<OrganizationDto> CreateAsync(CreateOrganizationDto dto);
        Task<bool> UpdateAsync(int id, UpdateOrganizationDto dto);
        Task<bool> DeleteAsync(int id);
    }
}