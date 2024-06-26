﻿using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Objectives.ResponseDto;
using Application.Objectives.Types.GetByIdWithPagination;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.GetObjectives.GetAllForCustomer
{
    public class
        GetAllObjectivesByCreatorCommandHandler : IQueryHandler<GetAllObjectivesByCreatorQuery,
        PaginationModel<ResponseObjectiveDto>>
    {
        private readonly IObjectiveRepository _repository;
        private readonly ILogger<GetAllObjectivesByCreatorCommandHandler> _logger;
        private readonly IMapper _mapper;

        public GetAllObjectivesByCreatorCommandHandler(IObjectiveRepository objectiveRepository,
            ILogger<GetAllObjectivesByCreatorCommandHandler> logger, IMapper mapper)
        {
            _repository = objectiveRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<PaginationModel<ResponseObjectiveDto>>> Handle(GetAllObjectivesByCreatorQuery request,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all objectives by creator has been requested");

            if (request.PageNum <= 0)
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<ResponseObjectiveDto>>("The page number must be greater than 0",
                    new Error(typeof(GetAllObjectivesByCreatorCommandHandler).Namespace!, "The page number must be greater than 0", 400));
            }

            var objectives = await _repository.GetByExpressionWithIncludesAndPaginationAsync(obj => obj.CreatorId == request.CreatorId, 
                request.PageSize, request.PageNum, cancellationToken, obj => obj.Categories, obj => obj.Creator, obj => obj.Type);

            if (!objectives.result.Any())
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<ResponseObjectiveDto>>("No objectives found",
                    new Error(typeof(GetAllObjectivesByCreatorCommandHandler).Namespace!, "No objectives found", 200));
            }

            var objectiveDtos = objectives.result.OrderByDescending(obj => obj.CreatedAt).Select(_mapper.Map<ResponseObjectiveDto>);

            var result = new PaginationModel<ResponseObjectiveDto>(objectives.count, objectiveDtos, request.PageNum, request.PageSize);

            return Result<PaginationModel<ResponseObjectiveDto>>.Success(result);
        }
    }
}