using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Domain.Types;
using Microsoft.Extensions.Logging;
using Shared;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Objectives.Types.GetById;

public class GetByIdQueryHandler : IQueryHandler<GetByIdQuery, ResponseTypeDto>
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
    
    public async Task<Result<ResponseTypeDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Get type request has been received for type with id {id}", request.Id);
        var result = await _typeRepository.GetByIdAsync(request.Id, cancellationToken);

        if (result is not null)
        {
            _logger.LogInformation("Type with Id = {id} successfully retrieved from the DB", request.Id);
            return Result<ResponseTypeDto>.Success(_mapper.Map<ResponseTypeDto>(result));
        }
        
        _logger.LogError("There are no type with id {id}", request.Id);
        ResponseHelper.LogAndReturnError<ResponseTypeDto>("Type not found", new Error(typeof(GetByIdQueryHandler).Namespace!, "", 404));

        return Result<ResponseTypeDto>.Success(_mapper.Map<ResponseTypeDto>(result));
    }
}