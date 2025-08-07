using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupportTicket.Application.Features.AdminControl.Commands.ActivateUserAccount
{
    public record ActivateUserAccountCommand(Guid AdminId, string TargetUserEmail) : IRequest<bool>;
}
