using MediatR;
using SupportTicket.Application.Features.Users.DTOs;
using SupportTicket.Application.Interfaces;
using SupportTicket.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.Users.Queries.GetMyProfile
{
    public class GetMyProfileQuery : IRequest<GetMyProfileDto>
    {
    }

}
