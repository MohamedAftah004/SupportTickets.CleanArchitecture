using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.TicketResponses.DTOs
{
    public class TicketResponseDto
    {
        public Guid Id { get; set; }
        public string ResponseText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Guid TicketId { get; set; }
        public Guid AdminUserId { get; set; }

        // Optional: For display purposes
        public string? AdminUserName { get; set; }
    }
}
