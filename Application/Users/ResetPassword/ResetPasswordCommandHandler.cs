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
using AutoMapper;
using Domain.Users.UserDetails;
using Microsoft.Extensions.Logging;

namespace Application.Users.ResetPassword;

public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ResetPasswordResponseDto>
{
	private readonly IUserCommunicationChannelRepository _userCommunicationChannelRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IHashProvider _hashProvider;
	private readonly IMapper _mapper;
	private readonly ILogger<ResetPasswordCommandHandler> _logger;

	public ResetPasswordCommandHandler(IUserCommunicationChannelRepository userCommunicationChannelRepository, IUnitOfWork unitOfWork, IHashProvider hashProvider,
		IMapper mapper, ILogger<ResetPasswordCommandHandler> logger)
	{
		_userCommunicationChannelRepository = userCommunicationChannelRepository;
		_unitOfWork = unitOfWork;
		_hashProvider = hashProvider;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<Result<ResetPasswordResponseDto>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Password reset requested for user");

		UserCommunicationChannel? channel = await _userCommunicationChannelRepository.GetByExpressionWithIncludesAsync(
			ucc => ucc.ConfirmationToken == request.Token && ucc.CommunicationChannel.Name == 
				CommunicationChannelName.BuildCommunicationChannelName(CommunicationChannelNameVariations.GetValue(CommunicationChannelNameVariations.Email).Value!).Value, 
			cancellationToken, ucc => ucc.User);

		if (channel is null)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("No match with the user and email communication channel found", UserErrors.EmailChannelMissing());
		}

		if (channel.ConfirmationToken != request.Token)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("Invalid token", UserErrors.InvalidConfirmationToken());
		}

		var password = Password.BuildPassword(request.Password);
		if (!password.IsSuccess)
		{
			return ResponseHelper.LogAndReturnError<ResetPasswordResponseDto>("Invalid password", password.Error);
		}
		
		password.Value!.Value = _hashProvider.GetHash(password.Value.Value);

		channel.User.Password = password.Value!;

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		_logger.LogInformation("Pasword reset successfull for user");
		return Result<ResetPasswordResponseDto>.Success(_mapper.Map<ResetPasswordResponseDto>(channel.User));
	}
}
