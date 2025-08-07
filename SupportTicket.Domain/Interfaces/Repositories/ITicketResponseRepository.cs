using SupportTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Domain.Interfaces.Repositories
{
    public interface ITicketResponseRepository
    {
        Task<TicketResponse?> GetByIdAsync(Guid id);
        Task<IEnumerable<TicketResponse>> GetByTicketIdAsync(Guid ticketId);
        Task<IEnumerable<TicketResponse>> GetByUserIdAsync(Guid userId); 
        Task AddAsync(TicketResponse response);
        Task UpdateAsync(TicketResponse response);
        Task DeleteAsync(Guid id);
    }
}
