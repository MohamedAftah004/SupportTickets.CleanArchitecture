using SupportTicket.Domain.Entities;
using SupportTicket.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Domain.Interfaces.Repositories
{
    public interface ITicketRepository
    {
        // Queries
        Task<Ticket?> GetByIdAsync(Guid id);
        Task<IEnumerable<Ticket>> GetAllAsync(); // For Admin
        Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId); // For user dashboard
        Task<IEnumerable<Ticket>> GetByStatusAsync(TicketStatus status); // For Admin filtering
        Task<IEnumerable<Ticket>> GetByUserIdAndStatusAsync(Guid userId, TicketStatus status); // Optional UX
        Task<IEnumerable<Ticket>> GetUnassignedTicketsAsync(); // For Admin assignment
        Task<IEnumerable<Ticket>> GetOpenTicketsAsync(); // For quick access to active tickets

        // Commands
        Task AddAsync(Ticket ticket); // Create
        Task UpdateAsync(Ticket ticket); // Edit or assign/change status
        Task DeleteAsync(Guid id); // Remove from DB

    }
}
