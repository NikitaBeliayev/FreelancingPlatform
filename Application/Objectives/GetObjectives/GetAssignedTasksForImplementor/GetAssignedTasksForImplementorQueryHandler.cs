using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Objectives.GetObjectives.GetAllWithPagiation;
using Application.Objectives.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;
using System.Runtime.CompilerServices;

namespace Application.Objectives.GetObjectives.GetAssignedTasksForImplementor
{
    public class
        GetAssignedTasksForImplementorQueryHandler : IQueryHandler<GetAssignedTasksForImplementorQuery,
        PaginationModel<TaskForYouDto>>
    {
        private readonly IObjectiveRepository _repository;
        private readonly ILogger<GetAssignedTasksForImplementorQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetAssignedTasksForImplementorQueryHandler(IObjectiveRepository objectiveRepository,
            ILogger<GetAssignedTasksForImplementorQueryHandler> logger, IMapper mapper)
        {
            _repository = objectiveRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<PaginationModel<TaskForYouDto>>> Handle(GetAssignedTasksForImplementorQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all assigned tasks for implementor has been requested");

            if (request.PageNum <= 0)
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<TaskForYouDto>>("The page number must be greater than 0",
                    new Error(typeof(GetAssignedTasksForImplementorQueryHandler).Namespace!, "The page number must be greater than 0", 400));
            }

            var objectives = await _repository.GetByExpressionWithIncludesAndPaginationAsync(obj => obj.Implementors.Any(i => i.Id == request.ImplementorId),
                request.PageSize, request.PageNum, cancellationToken, obj => obj.Categories, obj => obj.Creator, obj => obj.Type);

            if (!objectives.result.Any())
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<TaskForYouDto>>("No objectives found",
                    new Error(typeof(GetAssignedTasksForImplementorQueryHandler).Namespace!, "No objectives found",
                        200));
            }

            var objectiveDtos = objectives.result.OrderByDescending(obj => obj.CreatedAt).Select(_mapper.Map<TaskForYouDto>);

            var result = new PaginationModel<TaskForYouDto>(objectives.count, objectiveDtos, request.PageNum, request.PageSize);

            return Result<PaginationModel<TaskForYouDto>>.Success(result);
        }
    }
}