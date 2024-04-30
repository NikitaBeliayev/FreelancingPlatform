using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Users.GetById
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper, ILogger<GetUserByIdQueryHandler> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<UserDto>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User get requested");
            var result = await _userRepository.GetByIdAsync(query.UserId, cancellationToken);

            if (result != null)
            {
                _logger.LogInformation("User with Id = {id} successfully retrieved from the DB", query.UserId);
            }
            else
            {
                _logger.LogInformation("User not found in DB");
            }

            return Result<UserDto>.Success(_mapper.Map<UserDto>(result));
        }
    }
}
