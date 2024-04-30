using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.PaginatedResult;
using Application.Objectives.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.GetObjectives.GetAllWithPagiation
{
    public class GetAllObjectivesWithPaginationQueryHandler : IQueryHandler<GetAllObjectivesWithPaginationQuery, PaginatedResultDto<ResponseObjectiveDto>>
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

		public async Task<Result<PaginatedResultDto<ResponseObjectiveDto>>> Handle(GetAllObjectivesWithPaginationQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Get all objective types with pagination has been requested");
			var objectives = await _repository.GetAllForImplementorWithPagination(request.PageSize, (request.PageNum - 1) * request.PageSize, cancellationToken);
			if (!objectives.Any())
			{
				return ResponseHelper.LogAndReturnError<PaginatedResultDto<ResponseObjectiveDto>>("No objectives found", new Error("Objective GetObjectives.GetAllWithPagiation.GetAllObjectivesWithPaginationQueryHandler", "No objectives found", 422));
			}


			var objectiveDtos = objectives.Select(_mapper.Map<ResponseObjectiveDto>);

			var totalObjectives = await _repository.GetTotalCountForImplementor(cancellationToken); 
			var totalPages = (int)Math.Ceiling((double)totalObjectives / request.PageSize);

			var next = request.PageNum < totalPages ? $"https://<server>/api/tasks/?pageNum={request.PageNum + 1}&pageSize={request.PageSize}" : null;
			var previous = request.PageNum > 1 ? $"https://<server>/api/tasks/?pageNum={request.PageNum - 1}&pageSize={request.PageSize}" : null;

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
