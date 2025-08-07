using MediatR;
using SupportTicket.Application.Features.AdminControl.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Queries.GetAllUsers
{
    public record GetAdminsQuery():IRequest<List<UserDto>>;
}
