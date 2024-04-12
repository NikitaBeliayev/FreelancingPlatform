using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain;
using Domain.Repositories;
using Domain.Types;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Types.Update;

public class UpdateObjectiveTypeCommandHandler : ICommandHandler<UpdateObjectiveTypeCommand, ResponseTypeDto>
{
    private readonly IObjectiveTypeRepository _typeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateObjectiveTypeCommandHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public UpdateObjectiveTypeCommandHandler(IObjectiveTypeRepository typeRepository, IMapper mapper, ILogger<UpdateObjectiveTypeCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _typeRepository = typeRepository;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<ResponseTypeDto>> Handle(UpdateObjectiveTypeCommand request, CancellationToken cancellationToken)
    {
        var requestDto = request.RequestDto;
        
        _logger.LogInformation("Update type request has been received for type with id {id}", requestDto.Id);
        var typeTitleResult = ObjectiveTypeTitle.BuildObjectiveTypeTitle(requestDto.Title);

        if (!typeTitleResult.IsSuccess)
        {
            _logger.LogInformation("Invalid title {title} for updating type with id: {id}", requestDto.Title, requestDto.Id);
            return ResponseHelper.LogAndReturnError<ResponseTypeDto>("Invalid title", typeTitleResult.Error);
        }
        
        var possibleType = await _typeRepository.GetByIdAsync(requestDto.Id, cancellationToken);
        if (possibleType is null)
        {
            _logger.LogError("Type with id {id} not found", requestDto.Id);
            return ResponseHelper.LogAndReturnError<ResponseTypeDto>("Type not found", new Error("", "", 404));
        }


        possibleType.TypeTitle = typeTitleResult.Value!;
        possibleType.Duration = requestDto.Duration;
        
        _typeRepository.Update(possibleType);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Type with id: {id} updated successfully", request.RequestDto.Id);
        
        return Result<ResponseTypeDto>.Success(_mapper.Map<ResponseTypeDto>(possibleType));
    }
}