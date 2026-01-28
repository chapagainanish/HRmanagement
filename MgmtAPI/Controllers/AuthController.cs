using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login with email and password
        /// POST: api/auth/login
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }
            return Ok(result);
        }

        /// <summary>
        /// Register a new user
        /// POST: api/auth/register
        /// </summary>
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponseDto>> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Login), result);
        }

        /// <summary>
        /// Get current user info (requires authentication)
        /// GET: api/auth/me
        /// </summary>
        [HttpGet("me")]
        [Authorize]
        public ActionResult GetCurrentUser()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            return Ok(new { userId, email, name, role });
        }
    }
}