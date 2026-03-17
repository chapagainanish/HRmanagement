using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendenceService _attendenceService;

        public AttendenceController(IAttendenceService attendenceService)
        {
            _attendenceService = attendenceService;
        }

        [HttpGet("get/by-employee/{employeeId}")]
        public async Task<ActionResult<IEnumerable<AttendenceDto>>> GetByEmployee(
            int employeeId,
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
        {
            var result = await _attendenceService.GetByEmployeeAsync(employeeId, from, to);
            return Ok(result);
        }
         
        [HttpGet("get/by-date")]
        public async Task<ActionResult<IEnumerable<AttendenceDto>>> GetByDate([FromQuery] DateTime date)
        {
            var result = await _attendenceService.GetByDateAsync(date);
            return Ok(result);
        }

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

        
        [HttpPut("update/{id}")]
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

        [HttpDelete("delete/{id}")]
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

