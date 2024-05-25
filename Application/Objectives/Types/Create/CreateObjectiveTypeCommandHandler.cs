using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain;
using Domain.Objectives;
using Domain.Repositories;
using Domain.Types;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Types.Create;

public class CreateObjectiveTypeCommandHandler : ICommandHandler<CreateObjectiveTypeCommand, ResponseTypeDto>
{
    private readonly IObjectiveTypeRepository _typeRepository;
    private readonly ILogger<CreateObjectiveTypeCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateObjectiveTypeCommandHandler(IObjectiveTypeRepository typeRepository,
        ILogger<CreateObjectiveTypeCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _typeRepository = typeRepository;
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ResponseTypeDto>> Handle(CreateObjectiveTypeCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Objective type creation has been requested");

        var typeDto = request.TypeDto;
        var titleBuildResult = ObjectiveTypeTitle.BuildObjectiveTypeTitle(typeDto.Id);
        if (!titleBuildResult.IsSuccess)
        {
            _logger.LogError("Error while building title for type {typeDto}", typeDto);
            return ResponseHelper.LogAndReturnError<ResponseTypeDto>("", new Error("", "", 500));
        }

        var objectiveTypeDurationResult = ObjectiveTypeDuration.BuildObjectiveTypeDuration(request.TypeDto.Duration);
        if (!objectiveTypeDurationResult.IsSuccess)
        {
            return Result<ResponseTypeDto>.Failure(null,
                new Error(typeof(CreateObjectiveTypeCommandHandler).Namespace!,
                    "Duration must be more or equal to 8 hours", 422));
        }

        var possibleObjectiveType =
            await _typeRepository.GetByExpressionWithIncludesAsync(type => type.TypeTitle == titleBuildResult.Value!,
                cancellationToken);
        if (possibleObjectiveType is not null)
        {
            _logger.LogError("Type with title {title} already exists", titleBuildResult.Value);
            return ResponseHelper.LogAndReturnError<ResponseTypeDto>("Type with this title already exists",
                new Error(typeof(CreateObjectiveTypeCommandHandler).Namespace!, "", 400));
        }

        var createdObjectiveType = await _typeRepository.CreateAsync(new ObjectiveType(typeDto.Id,
            new List<Objective>(),
            titleBuildResult.Value!, objectiveTypeDurationResult.Value, ""), cancellationToken); //plug to compile
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created user with Id = {Id}", createdObjectiveType.Id);
        return Result<ResponseTypeDto>.Success(_mapper.Map<ResponseTypeDto>(createdObjectiveType));
    }
}