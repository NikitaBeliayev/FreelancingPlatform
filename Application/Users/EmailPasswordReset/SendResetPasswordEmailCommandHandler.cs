using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Models;
using Domain.Repositories;
using Domain.UserCommunicationChannels;
using Domain.Users.UserDetails;
using Shared;
using Application.Users.ResponseDto;
using Microsoft.Extensions.Configuration;
using Application.Helpers;
using Domain.CommunicationChannels;
using Domain.Users.Errors;
using Application.Abstraction;
using Application.Users.Create;
using Microsoft.Extensions.Logging;

namespace Application.Users.EmailPasswordReset;

public class SendResetPasswordEmailCommandHandler : ICommandHandler<SendResetPasswordEmailCommand, ResetPasswordResponseDto>
{
	private readonly IUserCommunicationChannelRepository _userCommunicationChannelRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IConfiguration _configuration;
	private readonly IEmailProvider _emailProvider;
	private readonly ILogger<SendResetPasswordEmailCommandHandler> _logger;

	public SendResetPasswordEmailCommandHandler(IUserCommunicationChannelRepository userCommunicationChannelRepository, 
		IUnitOfWork unitOfWork, IConfiguration configuration, IEmailProvider emailProvider, ILogger<SendResetPasswordEmailCommandHandler> logger)
	{
		_userCommunicationChannelRepository = userCommunicationChannelRepository;
		_unitOfWork = unitOfWork;
		_configuration = configuration;
		_emailProvider = emailProvider;
		_logger = logger;
	}

	public async Task<Result<ResetPasswordResponseDto>> Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Reset password email sending has been requested for user with email = {UserEmail}", request.userEmail);

		var emailResult = EmailAddress.BuildEmail(request.userEmail);
		if (!emailResult.IsSuccess)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("Invalid email format", UserErrors.InvalidEmailFormat(request.userEmail));
		}

		UserCommunicationChannel? channel = await _userCommunicationChannelRepository.GetByExpressionWithIncludesAsync(c => c.CommunicationChannel.Type == CommunicationChannelType.Email && c.CommunicationChannel.UserCommunicationChannels.Any(ucc => ucc.User.Email.Equals(request.userEmail)), cancellationToken);

		if (channel is null)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("No match with the user and email communication channel found", UserErrors.EmailChannelMissing());
		}

		int minMinutesBetweenEmails = _configuration.GetSection("Email").GetValue<int>("ResendMinutesDelay");

		if (channel.LastEmailSentAt.HasValue && (DateTime.UtcNow - channel.LastEmailSentAt.Value).TotalMinutes < minMinutesBetweenEmails)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>($"Please wait at least {minMinutesBetweenEmails} minutes before requesting a new password reset email.", UserErrors.TooManyRequests(channel.UserId));
		}

		Guid resetToken = Guid.NewGuid();
		channel.ConfirmationToken = resetToken;
		channel.LastEmailSentAt = DateTime.UtcNow;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		EmailMessageComposer messageComposer = new EmailMessageComposer()
		{
			Recipient = channel.User.Email,
			ConfirmationEmail = new ConfirmationEmail()
			{
				ConfirmationToken = resetToken,
				UserId = channel.User.Id
			}
		};

		await _emailProvider.SendAsync(messageComposer, cancellationToken);

		_logger.LogInformation("Reset password email sent successfully to user: Id = {UserId}", channel.UserId);

		return Result<ResetPasswordResponseDto>.Success(new ResetPasswordResponseDto { Id = channel.UserId, Success = true, Message = "Reset password email sent" });
	}
}
