using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface ITravelExpenseService
    {

        Task<IEnumerable<TravelExpenseDto>> GetAllAsync();
        Task<TravelExpenseDto?> GetByIdAsync(int id);
        Task<TravelExpenseDto> CreateAsync(CreateTravelExpenseDto dto);
        Task<bool> UpdateAsync(int id, UpdateTravelExpenseDto dto);
        Task<bool> DeleteAsync(int id);

    }
}