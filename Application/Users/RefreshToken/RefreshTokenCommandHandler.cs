using Application.Abstraction.Messaging;
using Application.Abstraction;
using Application.Helpers;
using Application.Models.Jwt;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;
using System.Net;
using System.Security.Claims;
using Application.Users.ResponseDto;
using AutoMapper;
using Domain.Users;

namespace Application.Users.RefreshToken
{
    public class RefreshTokenCommandHandler : ICommandHandler<RefreshTokenCommand, UserLoginResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly IMapper _mappper;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;

        public RefreshTokenCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IMapper mappper, ILogger<RefreshTokenCommandHandler> logger)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
            _mappper = mappper;
            _logger = logger;
        }
        public async Task<Result<UserLoginResponseDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User refresh token requested");

            ClaimsPrincipal principal;
            try
            {
                principal = _jwtProvider.ValidateToken(request.Token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating token");
                return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Invalid token format", new Error("User.RefreshTokenCommandHandler",
                    "Invalid token format", (int)HttpStatusCode.Unauthorized));
            }

            Claim claim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!;

            if (claim == null)
            {
                _logger.LogError("Token does not contain 'sub' claim");
                return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Invalid token", new Error("User.RefreshTokenCommandHandler",
                    "Invalid token", (int)HttpStatusCode.Unauthorized));
            }

            Guid userId;
            if (!Guid.TryParse(claim.Value, out userId))
            {
                _logger.LogError("Could not parse user id from 'sub' claim");
                return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("Invalid user id in token", new Error("User.RefreshTokenCommandHandler",
                    "Invalid user id in token", (int)HttpStatusCode.Unauthorized));
            }

            User? possibleUser = await _userRepository.GetByExpressionWithIncludesAsync(
                user => user.Id == userId, cancellationToken, user => user.Roles);

            if (possibleUser is null)
            {
                _logger.LogError("No user found with id {UserId}", userId);
                return ResponseHelper.LogAndReturnError<UserLoginResponseDto>("There is no user with this id", new Error("User.RefreshTokenCommandHandler",
                    "There is no user with this id", (int)HttpStatusCode.Unauthorized));
            }

            JwtCredentials jwtCredentials = _jwtProvider.GenerateCredentials(possibleUser.Id, possibleUser.Email.Value,
                possibleUser.Roles.Select(r => r.Name.Value));


            _logger.LogInformation("User with Id = {Id} successfully refreshed token", possibleUser.Id);
            return Result<UserLoginResponseDto>.Success(_mappper.Map<UserLoginResponseDto>(
                new Tuple<User, JwtCredentials>(possibleUser, jwtCredentials)));
        }
    }
}
