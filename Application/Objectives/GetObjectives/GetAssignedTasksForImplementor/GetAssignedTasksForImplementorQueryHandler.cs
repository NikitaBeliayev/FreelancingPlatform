﻿using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Objectives.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.GetObjectives.GetAssignedTasksForImplementor
{
    public class GetAssignedTasksForImplementorQueryHandler : IQueryHandler<GetAssignedTasksForImplementorQuery, PaginationModel<ResponseObjectiveDto>>
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

        public async Task<Result<PaginationModel<ResponseObjectiveDto>>> Handle(GetAssignedTasksForImplementorQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all assigned tasks for implementor has been requested");
            var(objectives, total) = await _repository.GetByImplementorIdWithPagination(request.ImplementorId, request.PageSize, (request.PageNum - 1) * request.PageSize, cancellationToken);

            if (!objectives.Any())
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<ResponseObjectiveDto>>("No objectives found", new Error("Objective GetObjectives.GetAssignedTasksForImplementor.GetAssignedTasksForImplementorQueryHandler", "No objectives found", 500));
            }

            var objectiveDtos = objectives.Select(_mapper.Map<ResponseObjectiveDto>);

            var result = new PaginationModel<ResponseObjectiveDto>(total, objectiveDtos, request.PageNum, request.PageSize);

            return Result<PaginationModel<ResponseObjectiveDto>>.Success(result);
        }
    }
}