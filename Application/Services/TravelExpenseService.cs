using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class TravelExpenseService : ITravelExpenseService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TravelExpenseService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TravelExpenseDto>> GetAllAsync()
        {
            var items = await _unitOfWork.TravelExpences.GetAllAsync();

            var result = new List<TravelExpenseDto>();
            foreach (var item in items)
            {
                result.Add(MapToDto(item));
            }
            return result;
        }

        public async Task<TravelExpenseDto?> GetByIdAsync(int id)
        {
            var entity = await _unitOfWork.TravelExpences.GetByIdAsync(id);
            if (entity == null)
            {
                return null;
            }
            return MapToDto(entity);
        }

        public async Task<TravelExpenseDto> CreateAsync(CreateTravelExpenseDto dto)
        {
            var entity = new TravelExpense
            {
                EmployeeId = dto.EmployeeId,
                TravelDate = dto.TravelDate,
                Amount = dto.Amount,
                Purpose = dto.Purpose,
                Status = dto.Status
            };

            await _unitOfWork.TravelExpences.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            return MapToDto(entity);
        }

        public async Task<bool> UpdateAsync(int id, UpdateTravelExpenseDto dto)
        {
            var entity = await _unitOfWork.TravelExpences.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            if (dto.TravelDate.HasValue)
            {
                entity.TravelDate = dto.TravelDate.Value;
            }
            if (dto.Amount.HasValue)
            {
                entity.Amount = dto.Amount.Value;
            }
            if (dto.Purpose != null)
            {
                entity.Purpose = dto.Purpose;
            }
            if (dto.Status != null)
            {
                entity.Status = dto.Status;
            }

            _unitOfWork.TravelExpences.Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _unitOfWork.TravelExpences.GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _unitOfWork.TravelExpences.Remove(entity);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        private TravelExpenseDto MapToDto(TravelExpense entity)
        {
            return new TravelExpenseDto
            {
                TravelExpenseId = entity.TravelExpenceId,
                EmployeeId = entity.EmployeeId,
                Date = entity.TravelDate,
                Amount = entity.Amount,
                Purpose = entity.Purpose,
                Status = entity.Status,
                Description = entity.Purpose
            };
        }
    }
}