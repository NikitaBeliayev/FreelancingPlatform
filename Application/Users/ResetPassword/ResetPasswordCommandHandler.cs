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

namespace Application.Users.ResetPassword;

public class ResetPasswordCommandHandler : ICommandHandler<ResetPasswordCommand, ResetPasswordResponseDto>
{
	private readonly IUserCommunicationChannelRepository _userCommunicationChannelRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IHashProvider _hashProvider;
	private readonly IMapper _mapper;

	public ResetPasswordCommandHandler(IUserCommunicationChannelRepository userCommunicationChannelRepository, IUnitOfWork unitOfWork, IHashProvider hashProvider,
		IMapper mapper)
	{
		_userCommunicationChannelRepository = userCommunicationChannelRepository;
		_unitOfWork = unitOfWork;
		_hashProvider = hashProvider;
		_mapper = mapper;
	}

	public async Task<Result<ResetPasswordResponseDto>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
	{
		UserCommunicationChannel? channel = await _userCommunicationChannelRepository.GetByExpressionWithIncludesAsync(
			ucc => ucc.UserId == request.UserId && 
				ucc.CommunicationChannel.Name == 
				CommunicationChannelName.BuildCommunicationChannelName(CommunicationChannelNameVariations.GetValue(CommunicationChannelNameVariations.Email).Value!).Value, 
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

		return Result<ResetPasswordResponseDto>.Success(_mapper.Map<ResetPasswordResponseDto>(channel.User));
	}
}
