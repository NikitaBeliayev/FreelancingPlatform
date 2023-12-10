using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Domain.Users;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Users.GetById
{
    public class CreateUserCommandHundler : ICommandHundler<CreateUserCommand, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHundler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDTO>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var firstName = Name.BuildName(command.user.FirstName);
            if (!firstName.IsSuccess)
                return Result<UserDTO>.Failure(null, firstName.Error);

            var lastName = Name.BuildName(command.user.LastName);
            if (!lastName.IsSuccess)
                return Result<UserDTO>.Failure(null, lastName.Error);

            User newUser = new(Guid.NewGuid(), firstName.Value!, lastName.Value!);
            var result = await _userRepository.CreateAsync(newUser);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result != null
                ? Result<UserDTO>.Success(new UserDTO(result.Id, result.FirstName.Value, result.LastName.Value))
                : Result<UserDTO>.Failure(null, new Error("Users.CreateUserCommandHundler", "Something wrong"));
        }
    }
}
