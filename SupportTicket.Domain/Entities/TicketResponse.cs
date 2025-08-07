namespace SupportTicket.Domain.Entities
{
    public class TicketResponse
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ResponseText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Guid TicketId { get; set; }
        public Guid AdminUserId { get; set; }

        public bool IsDeleted {  get; set; } 

        
        //Nav Property
        public Ticket? Ticket { get; set; }
        public User? AdminUser { get; set; }

    }
}
