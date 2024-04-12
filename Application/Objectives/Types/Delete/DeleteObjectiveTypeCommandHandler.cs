using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Types.Delete;

public class DeleteObjectiveTypeCommandHandler : ICommandHandler<DeleteObjectiveTypeCommand, ResponseTypeDto>
{
    private readonly IObjectiveTypeRepository _typeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteObjectiveTypeCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public DeleteObjectiveTypeCommandHandler(IObjectiveTypeRepository typeRepository, IMapper mapper, 
        ILogger<DeleteObjectiveTypeCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _typeRepository = typeRepository;
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<ResponseTypeDto>> Handle(DeleteObjectiveTypeCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Delete type request has been received for type with id {id}", request.Id);
        var possibleObjectiveType = await _typeRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (possibleObjectiveType is null)
        {
            _logger.LogError("Type with id {id} not found", request.Id);
            return ResponseHelper.LogAndReturnError<ResponseTypeDto>("Type not found", new Error("", "", 404));
        }

        _typeRepository.Delete(possibleObjectiveType);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Type with id {id} has been deleted", request.Id);
        
        return Result<ResponseTypeDto>.Success(_mapper.Map<ResponseTypeDto>(possibleObjectiveType));
    }
}