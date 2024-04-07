using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using AutoMapper;
using Domain;
using Domain.Objectives;
using Domain.Repositories;
using Domain.Types;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Types.Create;

public class CreateObjectiveTypeCommandHandler : ICommandHandler<CreateObjectiveTypeCommand, TypeDto>
{
    private readonly IObjectiveTypeRepository _typeRepository;
    private readonly ILogger<CreateObjectiveTypeCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CreateObjectiveTypeCommandHandler(IObjectiveTypeRepository typeRepository, ILogger<CreateObjectiveTypeCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _typeRepository = typeRepository;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
   
    public async Task<Result<TypeDto>> Handle(CreateObjectiveTypeCommand request, CancellationToken cancellationToken)
    {
        var typeDto = request.TypeDto;
        var titleBuildResult = ObjectiveTypeTitle.BuildObjectiveTypeTitle(typeDto.TypeTitle);
        if (!titleBuildResult.IsSuccess)
        {
            _logger.LogError("Error while building title for type {typeDto}", typeDto);
            return ResponseHelper.LogAndReturnError<TypeDto>("", new Error("", "", 500));
        }
        
        var possibleObjectiveType = await _typeRepository.GetByExpressionWithIncludesAsync(type => type.TypeTitle == titleBuildResult.Value!, cancellationToken);
        if (possibleObjectiveType is not null)
        {
            _logger.LogError("Type with title {title} already exists", titleBuildResult.Value);
            return ResponseHelper.LogAndReturnError<TypeDto>("Type with this title already exists", new Error("", "", 400));
        }

        var createdObjectiveType = await _typeRepository.CreateAsync(new ObjectiveType(typeDto.Id, new List<Objective>(), 
            titleBuildResult.Value!, typeDto.Duration), cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<TypeDto>.Success(_mapper.Map<TypeDto>(createdObjectiveType));
    }
}