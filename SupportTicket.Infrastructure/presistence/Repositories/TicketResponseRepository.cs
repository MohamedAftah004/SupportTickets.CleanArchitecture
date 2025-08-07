using Microsoft.EntityFrameworkCore;
using SupportTicket.Domain.Entities;
using SupportTicket.Domain.Interfaces.Repositories;
using SupportTicket.Infrastructure.presistence.DbContexts;

namespace SupportTicket.Infrastructure.presistence.Repositories;

public class TicketResponseRepository : ITicketResponseRepository
{
    private readonly SupportTicketDbContext _context;

    public TicketResponseRepository(SupportTicketDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TicketResponse response)
    {
        await _context.TicketResponses.AddAsync(response);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var response = await _context.TicketResponses.FindAsync(id);
        if (response is not null)
        {
            _context.TicketResponses.Remove(response);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<TicketResponse?> GetByIdAsync(Guid id)
    {
        return await _context.TicketResponses
            .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
    }

    public async Task<IEnumerable<TicketResponse>> GetByTicketIdAsync(Guid ticketId)
    {
        return await _context.TicketResponses
            .Where(r => r.TicketId == ticketId && !r.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<TicketResponse>> GetByUserIdAsync(Guid userId)
    {
        return await _context.TicketResponses
            .Where(r => r.AdminUserId == userId && !r.IsDeleted)
            .ToListAsync();
    }

    public async Task UpdateAsync(TicketResponse response)
    {
        _context.TicketResponses.Update(response);
        await _context.SaveChangesAsync();
    }
}
