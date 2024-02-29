using Application.Abstraction;
using Application.Users;
using Application.Users.Create;
using Application.Users.EmailConfirm;
using Application.Users.EmailPasswordReset;
using Application.Users.GetById;
using Application.Users.ResendEmail;
using Application.Users.Register;
using Application.Users.Login;
using Application.Users.RequestDto;
using Application.Users.ResetPasswordDto;
using Application.Users.ResponseDto;
using Application.Users.ResetPassword;
using Domain.Roles;
using FreelancingPlatform.Middleware;
using FreelancingPlatform.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancingPlatform.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ISender _sender;
		public UserController(ISender sender)
		{
			_sender = sender;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
		{
			var command = new GetUserByIdQuery(id);
			var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

		[HttpPost()]
		public async Task<IActionResult> Post([FromBody] UserDto user, CancellationToken cancellationToken)
		{
			var command = new CreateUserCommand(user);
			var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

		[AllowAnonymous]
		[HttpPost("signup")]
		public async Task<IActionResult> Register([FromBody] UserRegistrationDto user, CancellationToken cancellationToken)
		{
			var command = new RegisterUserCommand(user);
			var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

		[AllowAnonymous]
		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLoginDto user, CancellationToken cancellationToken)
		{
			var command = new LoginUserCommand(user);
			var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{userId:guid}/Confirm/Email/{token:guid}")]
        public async Task<IActionResult> ConfirmEmail(Guid userId, Guid token, CancellationToken cancellationToken)
        {
            var command = new ConfirmUserEmailCommand(userId, token);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("ResendConfirmationEmail")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] ResendConfirmationEmailDto resendConfirmationEmailDto, CancellationToken cancellationToken)
        {
            var command = new ResendConfirmationEmailCommand(resendConfirmationEmailDto.userId);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

		[Authorize(Roles = nameof(RoleNameType.Admin))]
		[HttpPost("create")]
		public async Task<IActionResult> CreateUser([FromBody] UserDto user, CancellationToken cancellationToken)
		{
			var command = new CreateUserCommand(user);
			var result = await _sender.Send(command, cancellationToken);

			return Ok(result);
		}

		[AllowAnonymous]
		[HttpGet("{userEmail}/reset/email")]
		public async Task<IActionResult> SendResetPasswordEmail(string userEmail, CancellationToken cancellationToken)
		{
			var command = new SendResetPasswordEmailCommand(userEmail);
			var result = await _sender.Send(command, cancellationToken);

			return Ok(result);
		}

		[AllowAnonymous]
		[HttpPut("{userId:guid}/reset/email")]
		public async Task<IActionResult> ResetPassword(Guid userId, [FromBody] ResetPasswordDto resetPasswordDto, CancellationToken cancellationToken)
		{
			var command = new ResetPasswordCommand(userId, resetPasswordDto.Password, resetPasswordDto.Token);
			var result = await _sender.Send(command, cancellationToken);

			return Ok(result);
		}
	}
}