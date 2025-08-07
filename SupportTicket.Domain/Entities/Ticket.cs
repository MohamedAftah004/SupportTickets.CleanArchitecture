
using SupportTicket.Domain.Enums;

namespace SupportTicket.Domain.Entities
{
    public class Ticket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } = TicketStatus.New;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
        public Guid CreatedByUserId { get; set; }
        public Guid? AssignedByUserId { get; set; }

        //Navigation Property
        public User? CreatedByUser { get; set; }
        public User? AssignedByUser { get; set; }
        public ICollection<TicketResponse>? Responses { get; set; }



    }
}
