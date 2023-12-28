using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Users.GetById;
using AutoMapper;
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
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateUserQueryHandler> _logger;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<CreateUserQueryHandler> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User creation has been requested");

            var firstName = Name.BuildName(command.User.FirstName);
            if (!firstName.IsSuccess)
            {
                _logger.LogError("User creation failed, invalid firstname - {ErrorMessage}", firstName.Error.msg);
                return Result<UserDto>.Failure(null, firstName.Error);
            }

            var lastName = Name.BuildName(command.User.LastName);
            if (!lastName.IsSuccess)
            {
                _logger.LogError("User creation failed, invalid lastname - {ErrorMessage}", lastName.Error.msg);
                return Result<UserDto>.Failure(null, lastName.Error);
            }

            User newUser = new(Guid.NewGuid(), firstName.Value!, lastName.Value!);
            var result = await _userRepository.CreateAsync(newUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);


            if (result != null)
            {
                _logger.LogInformation("User with Id = {UserId} created successfully", command.User.Id);
                return Result<UserDto>.Success(_mapper.Map<UserDto>(result));
            }

            _logger.LogError("User creation failed, something wrong");
            return Result<UserDto>.Failure(null, new Error("Users.CreateUserCommandHundler", "Something wrong"));
        }
    }
}
