using Microsoft.EntityFrameworkCore;
using SupportTicket.Domain.Entities;
using SupportTicket.Domain.Enums;
using SupportTicket.Domain.Interfaces.Repositories;
using SupportTicket.Infrastructure.presistence.DbContexts;

namespace SupportTicket.Infrastructure.presistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly SupportTicketDbContext _context;

    public UserRepository(SupportTicketDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is not null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }

    //get all on system
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    //get all users
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _context.Users.Where(u => u.Role == UserRole.User).ToListAsync();
    }

    //get all admins
    public async Task<IEnumerable<User>> GetAdminsAsync()
    {
        return await _context.Users.Where(u => u.Role == UserRole.Admin).ToListAsync(); 
    }



    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
