using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MgmtAPI.Controllers
{
    /// <summary>
    /// Controller for managing employee attendance records
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendenceService _attendenceService;

        public AttendenceController(IAttendenceService attendenceService)
        {
            _attendenceService = attendenceService;
        }

        /// <summary>
        /// Get attendance records for a specific employee
        /// GET: api/attendence/by-employee/5?from=2024-01-01&to=2024-12-31
        /// </summary>
        [HttpGet("by-employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<AttendenceDto>>> GetByEmployee(
            int employeeId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var result = await _attendenceService.GetByEmployeeAsync(employeeId, from, to);
            return Ok(result);
        }
         
        /// <summary>
        /// Get all attendance records for a specific date
        /// GET: api/attendence/by-date?date=2024-01-15
        /// </summary>
        [HttpGet("by-date")]
        public async Task<ActionResult<IEnumerable<AttendenceDto>>> GetByDate([FromQuery] DateTime date)
        {
            var result = await _attendenceService.GetByDateAsync(date);
            return Ok(result);
        }

        /// <summary>
        /// Create a new attendance record
        /// POST: api/attendence
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<AttendenceDto>> Create([FromBody] CreateAttendenceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _attendenceService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetByDate), new { date = created.Date.Date }, created);
        }

        /// <summary>
        /// Update an existing attendance record
        /// PUT: api/attendence/5
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAttendenceDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _attendenceService.UpdateAsync(id, dto);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }

        /// <summary>
        /// Delete an attendance record
        /// DELETE: api/attendence/5
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _attendenceService.DeleteAsync(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}

