using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Users.ResponseDto;
using AutoMapper;
using Domain.CommunicationChannels;
using Domain.Repositories;
using Domain.UserCommunicationChannels;
using Domain.UserCommunicationChannels.Errors;
using Domain.Users.Errors;
using Shared;

namespace Application.Users.EmailConfirm;

public class ConfirmUserEmailCommandHandler : ICommandHandler<ConfirmUserEmailCommand, UserEmailConfirmationResponseDto>
{
    private readonly IUserCommunicationChannelRepository _userCommunicationChannelRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ConfirmUserEmailCommandHandler(IUserCommunicationChannelRepository userCommunicationChannelRepository,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userCommunicationChannelRepository = userCommunicationChannelRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UserEmailConfirmationResponseDto>> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
    {
        UserCommunicationChannel? channel = await _userCommunicationChannelRepository.GetByExpressionWithIncludesAsync(
            c => c.ConfirmationToken == request.UserConfirmEmailRequestDto.Token && 
            c.CommunicationChannel.Name == 
            CommunicationChannelName.BuildCommunicationChannelName(CommunicationChannelNameVariations.GetValue(CommunicationChannelNameVariations.Email).Value!).Value, 
            cancellationToken, 
            c => c.CommunicationChannel);
        if (channel is null)
        {
            return ResponseHelper.LogAndReturnError<UserEmailConfirmationResponseDto>("No match with the user and email communication channel found", 
                UserCommunicationChannelErrors.UserCommunicationChannelNotFound(request.UserConfirmEmailRequestDto.Token));
        }
        
        if (channel.IsConfirmed)
        {
            return ResponseHelper.LogAndReturnError<UserEmailConfirmationResponseDto>("Email is already confirmed", UserErrors.EmailAlreadyVerified(channel.UserId));
        }
        
        if (channel.ConfirmationToken != request.UserConfirmEmailRequestDto.Token)
        {
            return ResponseHelper.LogAndReturnError<UserEmailConfirmationResponseDto>("Invalid token", UserErrors.InvalidConfirmationToken(channel.UserId));
        }

        channel.IsConfirmed = true;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UserEmailConfirmationResponseDto>.Success(_mapper.Map<UserEmailConfirmationResponseDto>(channel.User));
    }
}