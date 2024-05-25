using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Objectives.GetObjectives.GetAllForCustomer;
using Application.Objectives.ResponseDto;
using AutoMapper;
using Domain.Objectives;
using Domain.Repositories;
using Domain.Roles;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objectives.GetObjective
{
    public class GetObjectiveQueryHandler : IQueryHandler<GetObjectiveQuery, GetObjectiveResponseDto>
    {
        private readonly IObjectiveRepository _repository;
        private readonly ILogger<GetObjectiveQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetObjectiveQueryHandler(IObjectiveRepository objectiveRepository,
            ILogger<GetObjectiveQueryHandler> logger, IMapper mapper)
        {
            _repository = objectiveRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Result<GetObjectiveResponseDto>> Handle(GetObjectiveQuery request,
            CancellationToken cancellationToken)
        {
            Objective? objective = null;

            _logger.LogInformation("Get objective with Id = {objectiveId} by {userRole} has been requested",
                request.objectiveId, request.userRole);

            if (RoleNameVariations.GetValue(RoleNameVariations.Customer).Value == request.userRole)
            {
                objective = await _repository.GetByExpressionWithIncludesAsync(
                    obj => obj.Id == request.objectiveId && obj.CreatorId == request.userId, cancellationToken,
                    objective => objective.Type, objective => objective.Categories, objective => objective.Creator);
            }
            else if (RoleNameVariations.GetValue(RoleNameVariations.Implementer).Value == request.userRole)
            {
                objective = await _repository.GetByExpressionWithIncludesAsync(
                    obj => obj.Id == request.objectiveId && (obj.Implementors.Any(imp => imp.Id == request.userId) ||
                                                             !obj.Implementors.Any()), cancellationToken,
                    objective => objective.Type, objective => objective.Categories, objective => objective.Creator);
            }

            if (objective is null)
            {
                return ResponseHelper.LogAndReturnError<GetObjectiveResponseDto>("No objective found",
                    new Error(typeof(GetObjectiveQueryHandler).Namespace!, "No objective found", 422));
            }

            var result = _mapper.Map<GetObjectiveResponseDto>(objective);

            _logger.LogInformation("Objective with Id = {id} retrived successfully", objective.Id);

            return Result<GetObjectiveResponseDto>.Success(result);
        }
    }
}