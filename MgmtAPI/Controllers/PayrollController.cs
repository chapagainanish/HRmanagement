using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollService _payrollService;

        public PayrollController(IPayrollService payrollService)
        {
            _payrollService = payrollService;
        }

        // GET: api/payroll/by-month?month=2024-01-01
        [HttpGet("by-month")]
        public async Task<ActionResult<IEnumerable<PayrollDto>>> GetByMonth([FromQuery] DateTime month)
        {
            var result = await _payrollService.GetByMonthAsync(month);
            return Ok(result);
        }

        // GET: api/payroll/5
        [HttpGet("/{id}")]
        public async Task<ActionResult<PayrollDto>> GetById(int id)
        {
            var payroll = await _payrollService.GetByIdAsync(id);
            if (payroll == null)
            {
                return NotFound();
            }
            return Ok(payroll);
        }

        // POST: api/payroll
        [HttpPost("CreatePayroll")]
        public async Task<ActionResult<PayrollDto>> Create([FromBody] CreatePayrollDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _payrollService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PayrollId }, created);
        }

        // PUT: api/payroll/5
        [HttpPut("UpdatePayroll/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePayrollDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _payrollService.UpdateAsync(id, dto);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/payroll/5
        [HttpDelete("deletepayroll/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _payrollService.DeleteAsync(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
