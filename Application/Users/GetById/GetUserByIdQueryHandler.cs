using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Domain.Users;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Users.GetById
{
    public class CreateUserQueryHandler : IQueryHandler<GetUserByIdQuery, UserDTO>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserQueryHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UserDTO>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetByIdAsync(query.UserId);

            return result != null
                ? Result<UserDTO>.Success(new UserDTO(result.Id, result.FirstName.Value, result.LastName.Value))
                : Result<UserDTO>.Success(null);
        }
    }
}
