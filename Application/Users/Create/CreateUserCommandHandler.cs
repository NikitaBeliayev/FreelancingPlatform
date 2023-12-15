using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Users.GetById;
using Domain.Users;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Users.Create
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserQueryHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<CreateUserQueryHandler> logger)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<UserDTO>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User creation has been requested");

            var firstName = Name.BuildName(command.User.FirstName);
            if (!firstName.IsSuccess)
            {
                _logger.LogError($"User creation failed, invalid firstname - {firstName.Error.msg}");
                return Result<UserDTO>.Failure(null, firstName.Error);
            }

            var lastName = Name.BuildName(command.User.LastName);
            if (!lastName.IsSuccess)
            {
                _logger.LogError($"User creation failed, invalid lastname - {lastName.Error.msg}");
                return Result<UserDTO>.Failure(null, lastName.Error);
            }

            User newUser = new(Guid.NewGuid(), firstName.Value!, lastName.Value!);
            var result = await _userRepository.CreateAsync(newUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            if (result != null)
            {
                _logger.LogInformation($"User with Id = {command.User.Id} created successfully");
                return Result<UserDTO>.Success(new UserDTO(result.Id, result.FirstName.Value, result.LastName.Value));
            }

            _logger.LogError("User creation failed, something wrong");
            return Result<UserDTO>.Failure(null, new Error("Users.CreateUserCommandHundler", "Something wrong"));
        }
    }
}
