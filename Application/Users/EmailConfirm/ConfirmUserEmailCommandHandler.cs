using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Users.ResponseDto;
using Domain.CommunicationChannels;
using Domain.Repositories;
using Domain.UserCommunicationChannels;
using Domain.Users.Errors;
using Shared;

namespace Application.Users.EmailConfirm;

public class ConfirmUserEmailCommandHandler : ICommandHandler<ConfirmUserEmailCommand, UserEmailConfirmationResponseDto>
{
    private readonly IUserCommunicationChannelRepository _userCommunicationChannelRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmUserEmailCommandHandler(IUserCommunicationChannelRepository userCommunicationChannelRepository, IUnitOfWork unitOfWork)
    {
        _userCommunicationChannelRepository = userCommunicationChannelRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserEmailConfirmationResponseDto>> Handle(ConfirmUserEmailCommand request, CancellationToken cancellationToken)
    {
        UserCommunicationChannel? channel = await _userCommunicationChannelRepository.GetByExpressionWithIncludesAsync(c => c.UserId == request.userId && c.CommunicationChannel.Name == CommunicationChannelName.BuildCommunicationChannelName(1).Value, cancellationToken, c => c.CommunicationChannel);
        if (channel is null)
        {
            return ResponseHelper.LogAndReturnError<UserEmailConfirmationResponseDto>("No match with the user and email communication channel found", UserErrors.EmailChannelMissing(request.userId));
        }
        
        if (channel.IsConfirmed)
        {
            return ResponseHelper.LogAndReturnError<UserEmailConfirmationResponseDto>("Email is already confirmed", UserErrors.EmailAlreadyVerified(request.userId));
        }
        
        if (channel.ConfirmationToken != request.token)
        {
            return ResponseHelper.LogAndReturnError<UserEmailConfirmationResponseDto>("Invalid token", UserErrors.InvalidConfirmationToken(request.userId));
        }

        channel.IsConfirmed = true;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<UserEmailConfirmationResponseDto>.Success(new UserEmailConfirmationResponseDto());
    }
}