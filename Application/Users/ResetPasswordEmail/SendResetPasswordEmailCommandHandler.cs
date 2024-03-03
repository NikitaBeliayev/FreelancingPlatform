using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Domain.Repositories;
using Domain.UserCommunicationChannels;
using Domain.Users.UserDetails;
using Shared;
using Application.Users.ResponseDto;
using Application.Helpers;
using Domain.CommunicationChannels;
using Domain.Users.Errors;
using Application.Abstraction;
using Application.Models.Email;
using Microsoft.Extensions.Logging;

namespace Application.Users.ResetPasswordEmail;

public class SendResetPasswordEmailCommandHandler : ICommandHandler<SendResetPasswordEmailCommand, ResetPasswordResponseDto>
{
	private readonly IUserCommunicationChannelRepository _userCommunicationChannelRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IEmailProvider _emailProvider;
	private readonly ILogger<SendResetPasswordEmailCommandHandler> _logger;

	public SendResetPasswordEmailCommandHandler(IUserCommunicationChannelRepository userCommunicationChannelRepository, 
		IUnitOfWork unitOfWork, IEmailProvider emailProvider, ILogger<SendResetPasswordEmailCommandHandler> logger)
	{
		_userCommunicationChannelRepository = userCommunicationChannelRepository;
		_unitOfWork = unitOfWork;
		_emailProvider = emailProvider;
		_logger = logger;
	}

	public async Task<Result<ResetPasswordResponseDto>> Handle(SendResetPasswordEmailCommand request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Reset password email sending has been requested for user with email = {UserEmail}", 
			request.UserResetPasswordEmailRequestDto.Email);

		var emailResult = EmailAddress.BuildEmail(request.UserResetPasswordEmailRequestDto.Email);
		if (!emailResult.IsSuccess)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("Invalid email format",
				UserErrors.InvalidEmailFormat(request.UserResetPasswordEmailRequestDto.Email));
		}
		
		UserCommunicationChannel? channel = await _userCommunicationChannelRepository.GetByExpressionWithIncludesAsync(
			ucc => ucc.CommunicationChannel.Name == CommunicationChannelName.BuildCommunicationChannelName(1).Value && 
			       ucc.CommunicationChannel.UserCommunicationChannels.Any(uccElement 
				       => uccElement.User.Email == emailResult.Value!), cancellationToken, 
			ucc => ucc.User);

		if (channel is null)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("No match with the user and email communication channel found", UserErrors.EmailChannelMissing());
		}

		int minMinutesBetweenEmails = _emailProvider.ResendMinutesDelay;

		if (channel.LastEmailSentAt.HasValue && (DateTime.UtcNow - channel.LastEmailSentAt.Value).TotalMinutes < minMinutesBetweenEmails)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>($"Please wait at least {minMinutesBetweenEmails} minutes before requesting a new password reset email.", UserErrors.TooManyRequests(channel.UserId));
		}

		Guid resetToken = Guid.NewGuid();
		channel.ConfirmationToken = resetToken;
		channel.LastEmailSentAt = DateTime.UtcNow;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		EmailMessageComposerModel messageComposerModel = new()
		{
			Recipient = channel.User.Email,
			Content = new ResetPasswordEmailModel()
			{
				ConfirmationToken = resetToken,
				EmailBody = _emailProvider.ResetPasswordEmailBody
			}
		};

		await _emailProvider.SendAsync(messageComposerModel, cancellationToken);

		_logger.LogInformation("Reset password email sent successfully to user: Id = {UserId}", channel.UserId);

		return Result<ResetPasswordResponseDto>.Success(new ResetPasswordResponseDto 
			{ Id = channel.UserId, Success = true, Message = "Reset password email sent" });
	}
}
