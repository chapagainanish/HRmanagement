using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelExpenseController : ControllerBase
    {
        private readonly ITravelExpenseService _travelExpenseService;

        public TravelExpenseController(ITravelExpenseService travelExpenseService)
        {
            _travelExpenseService = travelExpenseService;
        }

        // GET: api/travelexpense
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TravelExpenseDto>>> GetAll()
        {
            var result = await _travelExpenseService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/travelexpense/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TravelExpenseDto>> GetById(int id)
        {
            var expense = await _travelExpenseService.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return Ok(expense);
        }

        // POST: api/travelexpense
        [HttpPost]
        public async Task<ActionResult<TravelExpenseDto>> Create([FromBody] CreateTravelExpenseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _travelExpenseService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.TravelExpenseId }, created);
        }

        // PUT: api/travelexpense/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTravelExpenseDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _travelExpenseService.UpdateAsync(id, dto);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }

        // DELETE: api/travelexpense/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _travelExpenseService.DeleteAsync(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
