using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.TicketResponses.DTOs
{
    public class CreateTicketResponseDto
    {
        public string ResponseText { get; set; } = string.Empty;
        public Guid TicketId { get; set; }
    }
}
