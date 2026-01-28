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
    /// Service for managing employee attendance
    /// </summary>
    public class AttendenceService : IAttendenceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendenceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get attendance records for a specific employee
        /// </summary>
        public async Task<IEnumerable<AttendenceDto>> GetByEmployeeAsync(
            int employeeId,
            DateTime? from = null,
            DateTime? to = null)
        {
            // Get records from database
            var attendences = await _unitOfWork.Attendences.GetByEmployeeAsync(employeeId, from, to);

            // Convert to DTOs
            var result = new List<AttendenceDto>();
            foreach (var item in attendences)
            {
                var dto = MapToDto(item);
                result.Add(dto);
            }

            return result;
        }

        /// <summary>
        /// Get all attendance records for a specific date
        /// </summary>
        public async Task<IEnumerable<AttendenceDto>> GetByDateAsync(DateTime date)
        {
            // Get records from database
            var attendences = await _unitOfWork.Attendences.GetByDateAsync(date);

            // Convert to DTOs
            var result = new List<AttendenceDto>();
            foreach (var item in attendences)
            {
                var dto = MapToDto(item);
                result.Add(dto);
            }

            return result;
        }

        /// <summary>
        /// Create a new attendance record
        /// </summary>
        public async Task<AttendenceDto> CreateAsync(CreateAttendenceDto dto)
        {
            // Create entity from DTO
            var entity = new Attendence
            {
                EmployeeId = dto.EmployeeId,
                Date = dto.Date,
                CheckIn = dto.CheckIn ?? TimeSpan.Zero,
                CheckOut = dto.CheckOut ?? TimeSpan.Zero,
                IsPresent = dto.IsPresent
            };

            // Save to database
            await _unitOfWork.Attendences.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            // Return created record
            return MapToDto(entity);
        }

        /// <summary>
        /// Update an existing attendance record
        /// </summary>
        public async Task<bool> UpdateAsync(int attendanceId, UpdateAttendenceDto dto)
        {
            // Find existing record
            var entity = await _unitOfWork.Attendences.GetByIdAsync(attendanceId);
            if (entity == null)
            {
                return false;
            }

            // Update fields if provided
            if (dto.CheckIn.HasValue)
            {
                entity.CheckIn = dto.CheckIn.Value;
            }
            if (dto.CheckOut.HasValue)
            {
                entity.CheckOut = dto.CheckOut.Value;
            }
            if (dto.IsPresent.HasValue)
            {
                entity.IsPresent = dto.IsPresent.Value;
            }

            // Save changes
            _unitOfWork.Attendences.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Delete an attendance record
        /// </summary>
        public async Task<bool> DeleteAsync(int attendanceId)
        {
            // Find existing record
            var entity = await _unitOfWork.Attendences.GetByIdAsync(attendanceId);
            if (entity == null)
            {
                return false;
            }

            // Delete from database
            _unitOfWork.Attendences.Remove(entity);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Convert entity to DTO
        /// </summary>
        private AttendenceDto MapToDto(Attendence entity)
        {
            var dto = new AttendenceDto
            {
                AttendanceId = entity.AttendanceId,
                EmployeeId = entity.EmployeeId,
                Date = entity.Date,
                CheckIn = entity.CheckIn,
                CheckOut = entity.CheckOut,
                IsPresent = entity.IsPresent
            };
            return dto;
        }
    }
}