using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Users.ResponseDto;
using Domain.Repositories;
using Domain.UserCommunicationChannels;
using Domain.Users.Errors;
using Domain.Users;
using Domain.CommunicationChannels;
using Shared;
using Application.Abstraction;
using Domain.Users.UserDetails;

namespace Application.Users.ResetPassword;

public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ResetPasswordResponseDto>
{
	private readonly IUserCommunicationChannelRepository _userCommunicationChannelRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IHashProvider _hashProvider;

	public ResetPasswordCommandHandler(IUserCommunicationChannelRepository userCommunicationChannelRepository, IUnitOfWork unitOfWork, IHashProvider hashProvider)
	{
		_userCommunicationChannelRepository = userCommunicationChannelRepository;
		_unitOfWork = unitOfWork;
		_hashProvider = hashProvider;
	}

	public async Task<Result<ResetPasswordResponseDto>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
	{
		UserCommunicationChannel? channel = await _userCommunicationChannelRepository.GetByExpressionWithIncludesAsync(
			ucc => ucc.UserId == request.UserId && 
				ucc.CommunicationChannel.Name == CommunicationChannelName.BuildCommunicationChannelName(1 ).Value, 
			cancellationToken, ucc => ucc.User);

		if (channel is null)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("No match with the user and email communication channel found", UserErrors.EmailChannelMissing(request.UserId));
		}

		if (channel.ConfirmationToken != request.Token)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("Invalid token", UserErrors.InvalidConfirmationToken(request.UserId));
		}

		var password = Password.BuildPassword(request.Password);
		if (!password.IsSuccess)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("Invalid password", password.Error);
		}
		
		password.Value!.Value = _hashProvider.GetHash(password.Value.Value);

		channel.User.Password = password.Value!;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result<ResetPasswordResponseDto>.Success(new ResetPasswordResponseDto { Id = channel.UserId, Success = true, Message = "Password reset successful" });
	}
}
