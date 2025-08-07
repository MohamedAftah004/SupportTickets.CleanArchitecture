using SupportTicket.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Interfaces.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}
