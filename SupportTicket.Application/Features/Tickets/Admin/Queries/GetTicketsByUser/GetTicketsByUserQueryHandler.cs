using MediatR;
using SupportTicket.Application.Features.Tickets.Admin.DTOs;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Tickets.Admin.Queries.GetTicketsByUser
{
    public class GetTicketsByUserQueryHandler : IRequestHandler<GetTicketsByUserQuery, List<TicketDto>>
    {


        private readonly ITicketRepository _ticketRepository;
        private readonly IUserRepository _userRepository;

        public GetTicketsByUserQueryHandler(ITicketRepository ticketRepository, IUserRepository userRepository)
        {
            _ticketRepository = ticketRepository;
            _userRepository = userRepository;
        }


        public async Task<List<TicketDto>> Handle(GetTicketsByUserQuery request, CancellationToken cancellationToken)
        {

            var ticketList = await _ticketRepository.GetByUserIdAsync(request.UserId);
            if (ticketList == null || !ticketList.Any())
                throw new Exception("No tickets found in this user account");

            return ticketList.Select(t => new TicketDto
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status.ToString(),
                CreatedAt = t.CreatedAt,
                UpdatedAt = t.UpdatedAt
            }).ToList();


        }
    }
}
