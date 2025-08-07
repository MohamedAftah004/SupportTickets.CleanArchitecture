namespace SupportTicket.Application.Features.TicketResponses.DTOs
{
    public class UpdateTicketResponseDto
    {
        public Guid Id { get; set; }
        public string ResponseText { get; set; } = string.Empty;
    }
}
