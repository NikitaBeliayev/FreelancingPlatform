using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using AutoMapper;
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
    public class CreateUserQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateUserQueryHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetByIdAsync(query.UserId);

            return result != null
                ? Result<UserDto>.Success(_mapper.Map<UserDto>(result))
                : Result<UserDto>.Success(null);
        }
    }
}
