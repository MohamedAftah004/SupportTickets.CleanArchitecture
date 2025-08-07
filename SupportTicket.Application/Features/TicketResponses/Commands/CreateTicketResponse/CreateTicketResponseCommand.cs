using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using SupportTicket.Application.Features.TicketResponses.DTOs;

namespace SupportTicket.Application.Features.TicketResponses.Commands.CreateTicketResponse
{
    public record CreateTicketResponseCommand(CreateTicketResponseDto CreateTicketResponse) : IRequest<CreateTicketResponseDto>;
   
}
