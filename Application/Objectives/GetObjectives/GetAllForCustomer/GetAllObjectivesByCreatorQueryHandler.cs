using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.PaginatedResult;
using Application.Objectives.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.GetObjectives.GetAllForCustomer
{
    public class GetAllObjectivesByCreatorCommandHandler : IQueryHandler<GetAllObjectivesByCreatorQuery, PaginatedResultDto<ResponseObjectiveDto>>
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

        public async Task<Result<PaginatedResultDto<ResponseObjectiveDto>>> Handle(GetAllObjectivesByCreatorQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all objectives by creator has been requested");
            var objectives = await _repository.GetByCreatorIdWithPagination(request.CreatorId, request.PageSize, (request.PageNum - 1) * request.PageSize, cancellationToken);
            if (!objectives.Any())
            {
                return ResponseHelper.LogAndReturnError<PaginatedResultDto<ResponseObjectiveDto>>("No objectives found", new Error("Objective GetObjectives.GetAllForCustomer.GetAllObjectivesByCreatorQueryHandler", "No objectives found", 500));
            }

            var objectiveDtos = objectives.Select(_mapper.Map<ResponseObjectiveDto>);

            var totalObjectives = await _repository.GetTotalCountForCreator(request.CreatorId, cancellationToken);
            var totalPages = (int)Math.Ceiling((double)totalObjectives / request.PageSize);

            var next = request.PageNum < totalPages ? $"https://<server>/api/creators/tasks/?pageNum={request.PageNum + 1}&pageSize={request.PageSize}" : null;
            var previous = request.PageNum > 1 ? $"https://<server>/api/creators/tasks/?pageNum={request.PageNum - 1}&pageSize={request.PageSize}" : null;

            var result = new PaginatedResultDto<ResponseObjectiveDto>
            {
                Count = totalObjectives,
                Next = next,
                Previous = previous,
                Results = objectiveDtos
            };

            return Result<PaginatedResultDto<ResponseObjectiveDto>>.Success(result);
        }
    }
}
