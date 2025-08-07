using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportTicket.Application.Features.AdminControl.Commands.ActivateUserAccount;
using SupportTicket.Application.Features.AdminControl.Commands.DeActivateUserAccount;
using SupportTicket.Application.Features.AdminControl.Commands.UpdateUserProfileByAdmin;
using SupportTicket.Application.Features.AdminControl.DTOs;
using SupportTicket.Application.Features.AdminControl.Queries.GetAllUsers;
using SupportTicket.Application.Features.AdminControl.Queries.GetUsers;

namespace SupportTicket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //update user profile
        [HttpPut("update-user-by-email")]
        public async Task<IActionResult> UpdateUserByEmail(UpdateUserProfileCommand command)
        {
            await _mediator.Send(command);
            return Ok("User updated successfully.");
        }


        // activate

        [HttpPut("activate")]
        public async Task<IActionResult> ActivateUser([FromBody] ActivateUserDto dto)
        {
            var userId = User.FindFirst("uid")?.Value;

            if (string.IsNullOrWhiteSpace(userId) || !Guid.TryParse(userId, out var adminId))
                return Unauthorized("Invalid token.");

            await _mediator.Send(new ActivateUserAccountCommand(adminId, dto.TargetUserEmail));
            return NoContent();
        }

        [HttpPut("deactivate")]
        public async Task<IActionResult> DeactivateUser([FromBody] ActivateUserDto dto)
        {
            var userId = User.FindFirst("uid")?.Value;

            if (string.IsNullOrWhiteSpace(userId) || !Guid.TryParse(userId, out var adminId))
                return Unauthorized("Invalid token.");

            await _mediator.Send(new DeActivateUserAccountCommand(adminId, dto.TargetUserEmail));
            return NoContent();
        }

        //get admins
        [HttpGet("admins")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAdmins()
        {
            var result = await _mediator.Send(new GetAdminsQuery());
            return Ok(result);
        }
        //get users
        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        //get All (admin/users)
        [HttpGet("admins-users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }




    }
}
