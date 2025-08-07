using SupportTicket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task<IEnumerable<User>> GetAdminsAsync();

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);



    }
}
