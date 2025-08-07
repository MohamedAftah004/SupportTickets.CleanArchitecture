using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SupportTicket.Application.Features.TicketResponses.Commands.CreateTicketResponse;
using SupportTicket.Application.Features.TicketResponses.Commands.DeleteTicketResponse;
using SupportTicket.Application.Features.TicketResponses.Commands.UpdateTicketResponse;
using SupportTicket.Application.Features.TicketResponses.DTOs;
using SupportTicket.Application.Features.TicketResponses.Queries.GetMyTicketResponses;
using SupportTicket.Application.Features.TicketResponses.Queries.GetTicketResponsesByTicket;

namespace SupportTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TicketResponsesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketResponsesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //create ticket response 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketResponseDto dto)
        {
            var command = new CreateTicketResponseCommand(dto);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketResponseDto dto)
        {
            if (id != dto.Id)
                return BadRequest("Mismatched response ID.");

            var command = new UpdateTicketResponseCommand(dto);
            var success = await _mediator.Send(command);

            if (!success)
                return NotFound("Ticket response not found or you are not the owner.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteTicketResponseCommand(id));

            if (!result)
                return NotFound("Ticket response not found or already deleted.");

            return NoContent();
        }


        [HttpGet("ticket/{ticketId}")]
        public async Task<IActionResult> GetByTicket(Guid ticketId)
        {
            var result = await _mediator.Send(new GetTicketResponsesByTicketQuery(ticketId));
            return Ok(result);
        }

        [HttpGet("my-responses")]
        public async Task<IActionResult> GetMyResponses()
        {
            var result = await _mediator.Send(new GetMyTicketResponsesQuery());
            return Ok(result);
        }

    }
}
