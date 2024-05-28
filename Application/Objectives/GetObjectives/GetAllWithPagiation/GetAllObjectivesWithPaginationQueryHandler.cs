using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.ResponseDto;
using AutoMapper;
using Application.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;
using Application.Objectives.Types.GetByIdWithPagination;
using Application.Objectives.Types.ResponseDto;

namespace Application.Objectives.GetObjectives.GetAllWithPagiation
{
    public class GetAllObjectivesWithPaginationQueryHandler : IQueryHandler<GetAllObjectivesWithPaginationQuery,
        PaginationModel<ResponseObjectiveDto>>
    {
        private readonly ILogger<GetAllObjectivesWithPaginationQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IObjectiveRepository _repository;

        public GetAllObjectivesWithPaginationQueryHandler(ILogger<GetAllObjectivesWithPaginationQueryHandler> logger,
            IMapper mapper, IObjectiveRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<PaginationModel<ResponseObjectiveDto>>> Handle(
            GetAllObjectivesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all objective types with pagination has been requested");

            if (request.PageNum <= 0)
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<ResponseObjectiveDto>>("The page number must be greater than 0",
                    new Error(typeof(GetAllObjectivesWithPaginationQueryHandler).Namespace!, "The page number must be greater than 0", 400));
            }

            var objectives = await _repository.GetAllWithIncludesAndPaginationAsync(request.PageSize, request.PageNum, cancellationToken,
                obj => obj.Categories, obj => obj.Creator, obj => obj.Type);

            if (!objectives.result.Any())
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<ResponseObjectiveDto>>("No objectives found",
                    new Error(typeof(GetAllObjectivesWithPaginationQueryHandler).Namespace!,
                        "No objectives found", 200));
            }

            var objectiveDtos = objectives.result.OrderByDescending(obj => obj.CreatedAt).Select(_mapper.Map<ResponseObjectiveDto>);

            var result = new PaginationModel<ResponseObjectiveDto>(objectives.count, objectiveDtos, request.PageNum, request.PageSize);

            return Result<PaginationModel<ResponseObjectiveDto>>.Success(result);
        }
    }
}