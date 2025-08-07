using SupportTicket.Domain.Enums;

namespace SupportTicket.Domain.Entities
{
    public class User
    { 
        public Guid Id { get; set; } = Guid.NewGuid();      
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash {  get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User; 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;

        //Navigation Property
        public ICollection<Ticket>? CreatedTickets { get; set; }
        public ICollection<Ticket>? AssignedTickets { get; set; }

    }
}
