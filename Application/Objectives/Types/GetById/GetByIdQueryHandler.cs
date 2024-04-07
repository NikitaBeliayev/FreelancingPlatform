using Application.Abstraction.Messaging;
using Application.Helpers;
using AutoMapper;
using Domain.Repositories;
using Domain.Types;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Types.GetById;

public class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, TypeDto>
{
    
    private readonly IObjectiveTypeRepository _typeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetByIdQueryHandler> _logger;
    
    public GetByIdQueryHandler(IObjectiveTypeRepository typeRepository, IMapper mapper, ILogger<GetByIdQueryHandler> logger)
    {
        _typeRepository = typeRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    public async Task<Result<TypeDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get type request has been received for type with id {id}", request.Id);
        var result = await _typeRepository.GetByIdAsync(request.Id, cancellationToken);

        if (result is not null) return Result<TypeDto>.Success(_mapper.Map<TypeDto>(result));
        
        
        _logger.LogError("There are no type with id {id}", request.Id);
        ResponseHelper.LogAndReturnError<TypeDto>("Type not found", new Error("", "", 404));

        return Result<TypeDto>.Success(_mapper.Map<TypeDto>(result));
    }
}