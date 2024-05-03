using System.Collections.Generic;
using System.Numerics;
using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Models;
using Application.Objectives.Types.GetById;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Categories;
using Domain.Repositories;
using Domain.Types;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Types.GetByIdWithPagination;

public class GetAllObjectiveTypesWithPaginationQueryHandler : IQueryHandler<GetAllObjectiveTypesWithPaginationQuery, PaginationModel<ResponseTypeDto>>
{
    private readonly ILogger<GetAllObjectiveTypesWithPaginationQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IObjectiveTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public GetAllObjectiveTypesWithPaginationQueryHandler(ILogger<GetAllObjectiveTypesWithPaginationQueryHandler> logger, IMapper mapper, IObjectiveTypeRepository repository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<PaginationModel<ResponseTypeDto>>> Handle(GetAllObjectiveTypesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get all objective types with pagination has been requested");
        var (types, total) = await _repository.GetAllWithPagination(request.pageSize, (request.pageNum - 1) * request.pageSize, cancellationToken);
        var response = new List<ResponseTypeDto>();
        
        await foreach (var objectiveType in types)
        {
            response.Add(new ResponseTypeDto() { Id = objectiveType.Id, Title = objectiveType.TypeTitle.Title });
        }

        var result = new PaginationModel<ResponseTypeDto>(total, response, request.pageNum, request.pageSize);

        return Result<PaginationModel<ResponseTypeDto>>.Success(result);
    }
}