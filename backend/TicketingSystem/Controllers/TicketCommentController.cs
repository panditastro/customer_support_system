// Controllers/TicketCommentController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketingSystem.DTOs;
using TicketingSystem.Interfaces;
using TicketingSystem.Services.Interfaces;

namespace TicketingSystem.Controllers;

[ApiController]
[Route("api/tickets/{ticketId}/comments")]
[Authorize] // Remove Roles restriction here to allow Agent/Admin if needed
public class TicketCommentController : ControllerBase
{
    private readonly ITicketCommentService _service;
    private readonly IAuthService _authService;

    public TicketCommentController(
        ITicketCommentService service,
        IAuthService authService)
    {
        _service = service;
        _authService = authService;
    }

    private int GetUserId() => _authService.GetUserId(User);

    [HttpPost]
    public async Task<IActionResult> AddComment(int ticketId, [FromBody] TicketCommentDto dto)
    {
        var userId = GetUserId();
        var comment = await _service.AddCommentAsync(ticketId, userId, dto);
        return CreatedAtAction(nameof(GetComments), new { ticketId }, comment);
    }

    [HttpGet]
    public async Task<IActionResult> GetComments(int ticketId)
    {
        var userId = GetUserId();
        var comments = await _service.GetCommentsAsync(ticketId, userId);
        return Ok(comments);
    }
}