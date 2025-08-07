using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SupportTicket.Application.Features.AdminControl.Commands.UpdateUserProfileByAdmin;
using SupportTicket.Application.Features.Users.Commands.ChangePassword;
using SupportTicket.Application.Features.Users.Commands.LoginUser;
using SupportTicket.Application.Features.Users.Commands.RegisterUser;
using SupportTicket.Application.Features.Users.Queries.GetMyProfile;

namespace SupportTicket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "User")]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //register
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserCommand command)
        {
            await _mediator.Send(command);
            return Ok("User registered successfully.");
        }

        //llogin
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //profile Details
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _mediator.Send(new GetMyProfileQuery());
            return Ok(result);
        }

        [HttpPut("update-profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileCommand command)
        {
            await _mediator.Send(command);
            return Ok("Profile updated successfully.");
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok("Password changed successfully.");
        }

    }
}
