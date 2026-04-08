using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.DTOs;
using TicketingSystem.Interfaces;
using TicketingSystem.Services.Interfaces;

namespace TicketingSystem.Controllers;

[ApiController]
[Route("api/tickets")]
[Authorize(Roles = "User")]
public class TicketController : ControllerBase
{
    private readonly ITicketService _service;
    private readonly IAuthService _authService;

public TicketController(ITicketService service, IAuthService authService)
{
    _service = service;
    _authService = authService; // 👈 assign
}

    /// <summary>
    /// Create a new ticket
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] TicketDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    message = "Invalid input",
                    errors = ModelState
                });
            }

            int userId = _authService.GetUserId(User);

            var ticket = await _service.CreateTicketAsync(userId, dto);

            return StatusCode(201, new
            {
                message = "Ticket created successfully",
                data = new
                {
                    ticket.Id,
                    ticket.Title,
                    ticket.Status,
                    ticket.Priority,
                    ticket.CreatedAt
                }
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Something went wrong",
                error = ex.Message
            });
        }
    }


   
[HttpGet("my")]
public async Task<IActionResult> GetMyTickets()
{
    try
    {
        int userId = _authService.GetUserId(User);

        var tickets = await _service.GetMyTicketsAsync(userId);

        return Ok(new
        {
            message = "Tickets fetched successfully",
            data = tickets
        });
    }
    catch (UnauthorizedAccessException ex)
    {
        return Unauthorized(new { message = ex.Message });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { message = "Something went wrong", error = ex.Message });
    }
}
}