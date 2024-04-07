using System.Numerics;
using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Objectives.Types.GetById;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Types.GetByIdWithPagination;

public class GetAllWithPaginationQueryHandler : IQueryHandler<GetAllWithPaginationQuery, IEnumerable<ResponseTypeDto>>
{
    private readonly ILogger<GetAllWithPaginationQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IObjectiveTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public GetAllWithPaginationQueryHandler(ILogger<GetAllWithPaginationQueryHandler> logger, IMapper mapper, IObjectiveTypeRepository repository, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<IEnumerable<ResponseTypeDto>>> Handle(GetAllWithPaginationQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get all objective types with pagination has been requested");
        var result = _repository.GetAllWithPagination(request.Take, request.Skip, cancellationToken);
        var response = new List<ResponseTypeDto>();
        
        await foreach (var objectiveType in result)
        {
            response.Add(new ResponseTypeDto() { Id = objectiveType.Id, Title = objectiveType.TypeTitle.Title });
        }

        return Result<IEnumerable<ResponseTypeDto>>.Success(response);
    }
}