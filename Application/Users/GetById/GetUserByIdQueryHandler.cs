using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using AutoMapper;
using Domain.Repositories;
using Shared;

namespace Application.Users.GetById
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);

            return Result<UserDto>.Success(_mapper.Map<UserDto>(result));
        }
    }
}
