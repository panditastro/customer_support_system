using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Services.Interfaces;
using TicketingSystem.DTOs;

namespace TicketingSystem.Controllers
{
    /// <summary>
    /// Handles authentication endpoints (Register & Login)
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            // ✅ Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.RegisterAsync(dto);

                return Ok(new
                {
                    success = true,
                    message = "User registered successfully",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        /// <summary>
        /// Login user
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            // ✅ Validation
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _service.LoginAsync(dto);

                return Ok(new
                {
                    success = true,
                    message = "Login successful",
                    data = result
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
    }
}