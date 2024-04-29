using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.GetObjectives.GetAllWithPagiation
{
    public class GetAllObjectivesWithPaginationQueryHandler : IQueryHandler<GetAllObjectivesWithPaginationQuery, IEnumerable<ResponseObjectiveDto>>
    {
        private readonly ILogger<GetAllObjectivesWithPaginationQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IObjectiveRepository _repository;

        public GetAllObjectivesWithPaginationQueryHandler(ILogger<GetAllObjectivesWithPaginationQueryHandler> logger, IMapper mapper, IObjectiveRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public Task<Result<IEnumerable<ResponseObjectiveDto>>> Handle(GetAllObjectivesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all objective types with pagination has been requested");
            var objectives = _repository.GetAllForImplementorWithPagination(request.Take, request.Skip, cancellationToken);
            var objectiveDtos = _mapper.Map<IEnumerable<ResponseObjectiveDto>>(objectives);
            if (!objectiveDtos.Any())
            {
                return Task.FromResult(ResponseHelper.LogAndReturnError<IEnumerable<ResponseObjectiveDto>>("No objectives found", new Error("Objective GetObjectives.GetAllWithPagiation.GetAllObjectivesWithPaginationQueryHandler", "No objectives found", 500)));
            }

            return Task.FromResult(Result<IEnumerable<ResponseObjectiveDto>>.Success(objectiveDtos));
        }
    }
}
