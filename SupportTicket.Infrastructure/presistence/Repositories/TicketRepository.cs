using Microsoft.EntityFrameworkCore;
using SupportTicket.Domain.Entities;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using SupportTicket.Infrastructure.presistence.DbContexts;
using System.Net.Sockets;
using SupportTicket.Domain.Entities;


namespace SupportTicket.Infrastructure.presistence.Repositories;

public class TicketRepository : ITicketRepository
{
    private readonly SupportTicketDbContext _context;

    public TicketRepository(SupportTicketDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Ticket ticket)
    {
        await _context.Tickets.AddAsync(ticket);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket is not null)
        {
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Ticket>> GetAllAsync()
    {
        return await _context.Tickets
            .Include(t => t.CreatedByUser)
            .Include(t => t.AssignedByUser)
            .Include(t => t.Responses)
        .ToListAsync();
    }

    public async Task<Ticket?> GetByIdAsync(Guid id)
    {
        return await _context.Tickets
            .Include(t => t.CreatedByUser)
            .Include(t => t.AssignedByUser)
            .Include(t => t.Responses)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<Ticket>> GetByStatusAsync(TicketStatus status)
    {
        return await _context.Tickets
            .Where(t => t.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByUserIdAsync(Guid userId)
    {
        return await _context.Tickets
            .Where(t => t.CreatedByUserId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetByUserIdAndStatusAsync(Guid userId, TicketStatus status)
    {
        return await _context.Tickets
            .Where(t => t.CreatedByUserId == userId && t.Status == status)
            .ToListAsync();
    }

    public async Task<IEnumerable<Ticket>> GetOpenTicketsAsync()
    {
        return await _context.Tickets
            .Where(t => t.Status != TicketStatus.Closed)
            .ToListAsync();
    }


    public async Task<IEnumerable<Ticket>> GetUnassignedTicketsAsync()
    {
        return await _context.Tickets
            .Where(t => t.Status != TicketStatus.Closed &&
                        !t.Responses.Any(r => r.AdminUser.Role == UserRole.Admin))
            .ToListAsync();
    }


    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Tickets.Update(ticket);
        await _context.SaveChangesAsync();
    }

   
}
