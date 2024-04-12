using Application.Users;
using Application.Users.Create;
using Application.Users.EmailConfirm;
using Application.Users.GetById;
using Application.Users.ResendEmail;
using Application.Users.Register;
using Application.Users.Login;
using Application.Users.RequestDto;
using Application.Users.ResetPassword;
using Application.Users.ResetPasswordEmail;
using Domain.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancingPlatform.Controllers
{
	[Authorize]
	[Route("api/users")]
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

		[AllowAnonymous]
		[HttpPost("signup")]
		public async Task<IActionResult> Register([FromBody] UserRegistrationDto user, CancellationToken cancellationToken)
		{
			var command = new RegisterUserCommand(user);
			var result = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Register), result);
        }

		[AllowAnonymous]
		[HttpPost("signin")]
		public async Task<IActionResult> Login([FromBody] UserLoginDto user, CancellationToken cancellationToken)
		{
			var command = new LoginUserCommand(user);
			var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("confirm/email")]
        public async Task<IActionResult> ConfirmEmail([FromBody] UserConfirmEmailRequestDto userConfirmEmailRequestDto, CancellationToken cancellationToken)
        {
            var command = new ConfirmUserEmailCommand(userConfirmEmailRequestDto);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("resend/confirm/email")]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] UserResendConfirmationEmailRequestDto userResendConfirmationEmailRequestDto, 
	        CancellationToken cancellationToken)
        {
            var command = new ResendConfirmationEmailCommand(userResendConfirmationEmailRequestDto.UserId);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

		[Authorize(Roles = "Admin")] // change this to variable in the future
		[AllowAnonymous]
		[HttpPost("create")]
		public async Task<IActionResult> CreateUser([FromBody] UserDto user, CancellationToken cancellationToken)
		{
			var command = new CreateUserCommand(user);
			var result = await _sender.Send(command, cancellationToken);

			return CreatedAtAction(nameof(CreateUser), result);
		}

		[AllowAnonymous]
		[HttpPost("reset/password")] // change url
		public async Task<IActionResult> SendResetPasswordEmail([FromBody] UserResetPasswordEmailRequestDto userEmail, CancellationToken cancellationToken)
		{
			var command = new SendResetPasswordEmailCommand(userEmail);
			var result = await _sender.Send(command, cancellationToken);

			return Ok(result);
		}

		[AllowAnonymous]
		[HttpPut("reset/password")]
		public async Task<IActionResult> ResetPassword([FromBody] UserResetPasswordRequestDto userResetPasswordRequestDto, CancellationToken cancellationToken)
		{
			var command = new ResetPasswordCommand(userResetPasswordRequestDto.Password, userResetPasswordRequestDto.Token);
			var result = await _sender.Send(command, cancellationToken);

			return Ok(result);
		}
	}
}