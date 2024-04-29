using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.GetObjectives.GetAllForCustomer
{
    public class GetAllObjectivesByCreatorCommandHandler : IQueryHandler<GetAllObjectivesByCreatorQuery, IEnumerable<ResponseObjectiveDto>>
    {
        private readonly IObjectiveRepository _repository;
        private readonly ILogger<GetAllObjectivesByCreatorCommandHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllObjectivesByCreatorCommandHandler(IObjectiveRepository objectiveRepository, ILogger<GetAllObjectivesByCreatorCommandHandler> logger, IMapper mapper)
        {
            _repository = objectiveRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ResponseObjectiveDto>>> Handle(GetAllObjectivesByCreatorQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all objectives by creator has been requested");
            var objectives = await _repository.GetByCreatorId(request.CreatorId, cancellationToken);
            var objectiveDtos = _mapper.Map<IEnumerable<ResponseObjectiveDto>>(objectives);
            if (!objectiveDtos.Any())
            {
                return ResponseHelper.LogAndReturnError<IEnumerable<ResponseObjectiveDto>>("No objectives found for the provided creator ID", new Error("Objective GetObjectives.GetAllForCustomer.GetAllObjectivesByCreatorQueryHandler", "No objectives found for the provided creator ID", 500));
            }

            return Result<IEnumerable<ResponseObjectiveDto>>.Success(objectiveDtos);
        }
    }
}
