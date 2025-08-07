using MediatR;
using SupportTicket.Application.Features.TicketResponses.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.TicketResponses.Commands.UpdateTicketResponse
{
    public record UpdateTicketResponseCommand(UpdateTicketResponseDto UpdateDto) : IRequest<bool>;
}
