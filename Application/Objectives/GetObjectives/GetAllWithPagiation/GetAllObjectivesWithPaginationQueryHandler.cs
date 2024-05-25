using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.ResponseDto;
using AutoMapper;
using Application.Models;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

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
            var (objectives, total) = await _repository.GetAllForImplementorWithPagination(request.PageSize,
                (request.PageNum - 1) * request.PageSize, cancellationToken);
            if (!objectives.Any())
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<ResponseObjectiveDto>>("No objectives found",
                    new Error(typeof(GetAllObjectivesWithPaginationQueryHandler).Namespace!,
                        "No objectives found", 200));
            }

            var objectiveDtos = objectives.Select(_mapper.Map<ResponseObjectiveDto>);

            var result =
                new PaginationModel<ResponseObjectiveDto>(total, objectiveDtos, request.PageNum, request.PageSize);

            return Result<PaginationModel<ResponseObjectiveDto>>.Success(result);
        }
    }
}