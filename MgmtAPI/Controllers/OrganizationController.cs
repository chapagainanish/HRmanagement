using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet("get/all")]
        public async Task<ActionResult<IEnumerable<OrganizationDto>>> GetAll()
        {
            var result = await _organizationService.GetAllAsync();
            return Ok(result);
        }

        // GET: api/organization/5
        [HttpGet("get/{id}")]
        public async Task<ActionResult<OrganizationDto>> GetById(int id)
        {
            var organization = await _organizationService.GetByIdAsync(id);
            if (organization == null)
            {
                return NotFound();
            }
            return Ok(organization);
        }

    
        [HttpPost("post")]
        public async Task<ActionResult<OrganizationDto>> Create([FromBody] CreateOrganizationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var created = await _organizationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.OrganizationId }, created);
        }

       
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOrganizationDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _organizationService.UpdateAsync(id, dto);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }

    
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _organizationService.DeleteAsync(id);
            if (success)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
