using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Shared;
using Domain.Roles;
using Domain.UserCommunicationChannels;
using Domain.Users.UserDetails;
using Application.Abstraction;
using Domain.Repositories;
using Domain.Users;

namespace Application.Users.Create
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IHashProvider _hashProvider;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<CreateUserCommandHandler> logger, IMapper mapper, IHashProvider hashProvider)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _hashProvider = hashProvider;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User creation has been requested");

            var email = Email.BuildEmail(command.User.Email);
            if (!email.IsSuccess)
            {
                return ResponseHelper.LogAndReturnError<UserDto>("Invalid email", email.Error);
            }

            var firstName = Name.BuildName(command.User.FirstName);
            if (!firstName.IsSuccess)
            {
                return ResponseHelper.LogAndReturnError<UserDto>("Invalid firstname", firstName.Error);
            }

            var lastName = Name.BuildName(command.User.LastName);
            if (!lastName.IsSuccess)
            {
                return ResponseHelper.LogAndReturnError<UserDto>("Invalid lastname", lastName.Error);
            }

            var password = Password.BuildPassword(command.User.Password);
            if (!password.IsSuccess)
            {
                return ResponseHelper.LogAndReturnError<UserDto>("Invalid password", password.Error);
            }
            
            password.Value!.Value = _hashProvider.GetHash(password.Value!.Value);

            User newUser = new(
                Guid.NewGuid(),
                email.Value!,
                firstName.Value!,
                lastName.Value!,
                password.Value!,
                new List<UserCommunicationChannel>(), 
                new List<Role>()
            );

            var result = await _userRepository.CreateAsync(newUser);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result != null)
            {
                _logger.LogInformation("User created successfully: Id = {UserId}", result.Id);
                return Result<UserDto>.Success(_mapper.Map<UserDto>(result));
            }

            return ResponseHelper.LogAndReturnError<UserDto>("User creation failed, something wrong", new Error("Users.CreateUserCommandHandler", "Something wrong", 500));
        }
    }
}