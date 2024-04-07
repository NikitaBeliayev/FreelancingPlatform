using Domain.Repositories;
using Application.Helpers;
using Application.Users.ResponseDto;
using Application.Abstraction;
using Shared;
using Microsoft.Extensions.Logging;
using Application.Abstraction.Messaging;
using Domain.CommunicationChannels;
using Application.Abstraction.Data;
using Application.Models.Email;
using AutoMapper;
using Domain.Users.Errors;

namespace Application.Users.ResendEmail
{
    public class ResendConfirmationEmailCommandHandler : ICommandHandler<ResendConfirmationEmailCommand, UserResendEmailConfirmationResponseDto>
    {
        private readonly ILogger<ResendConfirmationEmailCommandHandler> _logger;
        private readonly IEmailProvider _emailProvider;
        private readonly IUserCommunicationChannelRepository _userCommunicationChannelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ResendConfirmationEmailCommandHandler(ILogger<ResendConfirmationEmailCommandHandler> logger, 
            IEmailProvider emailProvider, IUserCommunicationChannelRepository userCommunicationChannelRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _emailProvider = emailProvider;
            _userCommunicationChannelRepository = userCommunicationChannelRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UserResendEmailConfirmationResponseDto>> Handle(ResendConfirmationEmailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Confirmation email sending has been requested for user with Id = {UserId}", request.userId);

            var userCommunicationChannel = await _userCommunicationChannelRepository.GetByExpressionWithIncludesAsync(
                ucc => ucc.UserId == request.userId && ucc.CommunicationChannelId == CommunicationChannelNameVariations.Email,
                cancellationToken,
                ucc => ucc.User);

            if (userCommunicationChannel == null)
            {
                return ResponseHelper.LogAndReturnError<UserResendEmailConfirmationResponseDto>("No match with the user and email communication channel found", UserErrors.EmailChannelMissing(request.userId));
            }

            if (userCommunicationChannel.IsConfirmed)
            {
                return ResponseHelper.LogAndReturnError<UserResendEmailConfirmationResponseDto>("Email is already confirmed", UserErrors.EmailAlreadyVerified(request.userId));
            }

            var now = DateTime.UtcNow;
            var lastSent = userCommunicationChannel.LastEmailSentAt ?? DateTime.MinValue;
            var resendDelay = TimeSpan.FromMinutes(_emailProvider.ResendMinutesDelay);

            if (now - lastSent < resendDelay)
            {
                return ResponseHelper.LogAndReturnError<UserResendEmailConfirmationResponseDto>("Email can't be sent yet", UserErrors.ResendEmailDelayNotMet(request.userId));
            }

            Guid confirmationToken = Guid.NewGuid();
            userCommunicationChannel.ConfirmationToken = confirmationToken;

            EmailMessageComposerModel messageComposerModel = new EmailMessageComposerModel()
            {
                Recipient = userCommunicationChannel.User.Email,
                Content = new ConfirmationEmailModel()
                {
                    ConfirmationToken = confirmationToken,
                    EmailBody = _emailProvider.ConfirmationEmailBody
                }
            };

            await _emailProvider.SendAsync(messageComposerModel, cancellationToken);

            userCommunicationChannel.LastEmailSentAt = DateTime.UtcNow;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Confirmation email sent successfully to user: Id = {UserId}", request.userId);

            return Result<UserResendEmailConfirmationResponseDto>.Success(_mapper.Map<UserResendEmailConfirmationResponseDto>(userCommunicationChannel.User));
        }
    }
}