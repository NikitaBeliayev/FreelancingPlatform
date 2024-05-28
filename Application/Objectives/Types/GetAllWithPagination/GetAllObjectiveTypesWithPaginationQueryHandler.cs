using System.Collections.Generic;
using System.Numerics;
using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Objectives.GetObjectives.GetAllForCustomer;
using Application.Objectives.ResponseDto;
using Application.Objectives.Types.GetById;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Categories;
using Domain.Objectives;
using Domain.Repositories;
using Domain.Types;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Types.GetByIdWithPagination;

public class GetAllObjectiveTypesWithPaginationQueryHandler : IQueryHandler<GetAllObjectiveTypesWithPaginationQuery,
    PaginationModel<ResponseTypeDto>>
{
    private readonly ILogger<GetAllObjectiveTypesWithPaginationQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IObjectiveTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public GetAllObjectiveTypesWithPaginationQueryHandler(
        ILogger<GetAllObjectiveTypesWithPaginationQueryHandler> logger, IMapper mapper,
        IObjectiveTypeRepository repository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PaginationModel<ResponseTypeDto>>> Handle(GetAllObjectiveTypesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get all objective types with pagination has been requested");

        if (request.pageNum <= 0)
        {
            return ResponseHelper.LogAndReturnError<PaginationModel<ResponseTypeDto>>("The page number must be greater than 0",
                new Error(typeof(GetAllObjectiveTypesWithPaginationQueryHandler).Namespace!, "The page number must be greater than 0", 400));
        }

        var types = await _repository.GetAllWithIncludesAndPaginationAsync(request.pageSize, request.pageNum, cancellationToken);


        if (!types.result.Any())
        {
            return ResponseHelper.LogAndReturnError<PaginationModel<ResponseTypeDto>>("No types found",
                new Error(typeof(GetAllObjectivesByCreatorCommandHandler).Namespace!, "No types found", 200));
        }

        var typesDtos = types.result.Select(_mapper.Map<ResponseTypeDto>);
        var result = new PaginationModel<ResponseTypeDto>(types.count, typesDtos, request.pageNum, request.pageSize);

        return Result<PaginationModel<ResponseTypeDto>>.Success(result);
    }
}