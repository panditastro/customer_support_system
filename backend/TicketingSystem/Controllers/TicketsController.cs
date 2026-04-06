using Microsoft.AspNetCore.Mvc;
using TicketingSystem.Interfaces;
using TicketingSystem.DTOs;

namespace TicketingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _service;

    public TicketsController(ITicketService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create(TicketDto dto)
    {
        var result = await _service.CreateTicketAsync(dto);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tickets = await _service.GetAllTicketsAsync();
        return Ok(tickets);
    }


[HttpGet("test-db")]
public IActionResult TestDb()
{
    return Ok("Database Connected");
}    

[HttpGet("test-jwt")]
public IActionResult TestJwt([FromServices] IConfiguration config)
{
    var key = config["Jwt:Key"];
    return Ok(key);
}

}