using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Services.Interfaces;
using TicketingSystem.DTOs;

namespace TicketingSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            var result = _service.Register(dto);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var result = _service.Login(dto);
            return Ok(result);
        }
    }
}