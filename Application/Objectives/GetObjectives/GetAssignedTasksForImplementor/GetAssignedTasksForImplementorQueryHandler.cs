using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.PaginatedResult;
using Application.Objectives.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.GetObjectives.GetAssignedTasksForImplementor
{
    public class GetAssignedTasksForImplementorQueryHandler : IQueryHandler<GetAssignedTasksForImplementorQuery, PaginatedResultDto<ResponseObjectiveDto>>
    {
        private readonly IObjectiveRepository _repository;
        private readonly ILogger<GetAssignedTasksForImplementorQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAssignedTasksForImplementorQueryHandler(IObjectiveRepository objectiveRepository, ILogger<GetAssignedTasksForImplementorQueryHandler> logger, IMapper mapper)
        {
            _repository = objectiveRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResultDto<ResponseObjectiveDto>>> Handle(GetAssignedTasksForImplementorQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all assigned tasks for implementor has been requested");
            var objectives = await _repository.GetByImplementorIdWithPagination(request.ImplementorId, request.PageSize, (request.PageNum - 1) * request.PageSize, cancellationToken);
            if (!objectives.Any())
            {
                return ResponseHelper.LogAndReturnError<PaginatedResultDto<ResponseObjectiveDto>>("No objectives found", new Error("Objective GetObjectives.GetAssignedTasksForImplementor.GetAssignedTasksForImplementorQueryHandler", "No objectives found", 500));
            }

            var objectiveDtos = objectives.Select(_mapper.Map<ResponseObjectiveDto>);

            var totalObjectives = await _repository.GetTotalCountForImplementorTasks(request.ImplementorId, cancellationToken);
            var totalPages = (int)Math.Ceiling((double)totalObjectives / request.PageSize);

            var next = request.PageNum < totalPages ? $"https://<server>/api/implementors/tasks/?pageNum={request.PageNum + 1}&pageSize={request.PageSize}" : null;
            var previous = request.PageNum > 1 ? $"https://<server>/api/implementors/tasks/?pageNum={request.PageNum - 1}&pageSize={request.PageSize}" : null;

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
