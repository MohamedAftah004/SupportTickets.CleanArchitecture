using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportTicket.Application.Features.Tickets.Admin.Commands.AssignTicket;
using SupportTicket.Application.Features.Tickets.Admin.Commands.ChangeTicketStatus;
using SupportTicket.Application.Features.Tickets.Admin.Commands.DeleteTicket;
using SupportTicket.Application.Features.Tickets.Admin.Queries.GetAllTickets;
using SupportTicket.Application.Features.Tickets.Admin.Queries.GetTicketsByStatus;
using SupportTicket.Application.Features.Tickets.Admin.Queries.GetTicketsByUser;
using SupportTicket.Application.Features.Tickets.Admin.Queries.GetUnassignedTickets;
using SupportTicket.Application.Features.Tickets.User.Commands.CloseTicket;
using SupportTicket.Application.Features.Tickets.User.Commands.CreateTicket;
using SupportTicket.Application.Features.Tickets.User.Commands.UpdateTicket;
using SupportTicket.Application.Features.Tickets.User.Queries.GetMyTickets;
using SupportTicket.Application.Features.Tickets.User.Queries.GetTicketById;

namespace SupportTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {

        private readonly IMediator _mediator;
        public TicketsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //˜˜˜˜˜˜˜˜˜˜˜˜˜˜EVERYONE Endpoints˜˜˜˜˜˜˜˜˜˜˜˜˜˜

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
        {
            var ticketId = await _mediator.Send(command);
            if (ticketId == Guid.Empty)
                return BadRequest("Failed to create ticket.");

            return CreatedAtAction(nameof(GetMyTickets), new { id = ticketId }, new { ticketId });
        }

        [HttpPut("update/{ticketId}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid ticketId, [FromBody] UpdateTicketCommand command)
        {
            if (ticketId != command.TicketId)
                return BadRequest("Route ID does not match body ID.");

            var isUpdated = await _mediator.Send(command);
            if (!isUpdated)
                return BadRequest(new { Message = "Ticket not found or unauthorized." });

            return NoContent();
        }


        [HttpPut("close/{ticketId}")]
        [Authorize]
        public async Task<IActionResult> Close(Guid ticketId, [FromBody] CloseTicketCommand command)
        {
            if (ticketId != command.TicketId)
                return BadRequest("Route ID does not match body ID.");

            var isClosed = await _mediator.Send(command);
            if (!isClosed)
                return BadRequest(new { Message = "Ticket not found or unauthorized." });

            return NoContent();
        }

        [HttpGet("my-tickets")]
        [Authorize]
        public async Task<IActionResult> GetMyTickets()
        {
            var tickets = await _mediator.Send(new GetMyTicketsQuery());
            return Ok(tickets);
        }

        [HttpGet("get/{ticketId}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid ticketId)
        {
            var ticket = await _mediator.Send(new GetTicketByIdQuery(ticketId));
            return Ok(ticket);
        }



        //˜˜˜˜˜˜˜˜˜˜˜˜˜˜Admin Endpoints˜˜˜˜˜˜˜˜˜˜˜˜˜˜

        [HttpPut("assign/{ticketId}")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Assign(Guid ticketId)
        {
            var result = await _mediator.Send(new AssignTicketCommand(ticketId));

            if(!result)
                return BadRequest(new { Message = "Failed to assign ticket. It may be closed or not found." });

            return NoContent();
        }

        [HttpPut("change-status/{ticketId}/{statusId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeStatus(Guid ticketId , short statusId)
        {
            var result = await _mediator.Send(new ChangeTicketStatusCommand(ticketId, statusId));

            if(!result)
                return BadRequest(new { Message = "Failed to change ticket status. It may not be found or is invalid." });

            return NoContent();
        }

        [HttpDelete("delete/{ticketId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid ticketId)
        {
            var result = await _mediator.Send(new DeleteTicketCommand(ticketId));

            if (!result)
                return BadRequest(new { Message = "Failed to delete ticket. It may not be found or is not closed." });

            return NoContent();
        }

     
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTicketsQuery());

            if (result == null || !result.Any())
                return NotFound(new { Message = "No tickets found." });

            return Ok(result);
        }

        [HttpGet("status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByStatus([FromQuery] short status)
        {
            var result = await _mediator.Send(new GetTicketsByStatusQuery(status));

            if (result == null || !result.Any())
                return NotFound(new { Message = "No tickets found with the selected status." });

            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByUserId(Guid userId)
        {
            var result = await _mediator.Send(new GetTicketsByUserQuery(userId));

            if (result == null || !result.Any())
                return NotFound(new { Message = "No tickets found with the selected user." });

            return Ok(result);
        }

        [HttpGet("unassigned")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetByUnassigned()
        {
            var result = await _mediator.Send(new GetUnassignedTicketsQuery());

            if (result == null || !result.Any())
                return NotFound(new { Message = "No unassigned tickets founded." });

            return Ok(result);
        }



    }
}
