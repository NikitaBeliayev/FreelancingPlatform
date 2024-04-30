﻿using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using AutoMapper;
using Domain.Categories;
using Domain.Objectives;
using Domain.Payments;
using Domain.Repositories;
using Domain.Statuses;
using Domain.Types;
using Domain.Users.Errors;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.CreateObjective;

public class CreateObjectiveCommandHandler : ICommandHandler<CreateObjectiveCommand, ObjectiveDto>
{
    private readonly IObjectiveRepository _objectiveRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CreateObjectiveCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUserRepository _userRepository;
    public CreateObjectiveCommandHandler(IObjectiveRepository objectiveRepository, IUnitOfWork unitOfWork, 
        IUserRepository userRepository, IMapper mapper, ILogger<CreateObjectiveCommandHandler> logger, ICategoryRepository categoryRepository)
    {
        _objectiveRepository = objectiveRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = logger;
        _categoryRepository = categoryRepository;
    }
   
    
    public async Task<Result<ObjectiveDto>> Handle(CreateObjectiveCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Objective creation requested");

        var titleResult = ObjectiveTitle.BuildName(request.RequestDto.Title);
        if (!titleResult.IsSuccess)
        {
            _logger.LogError("Error: Invalid title, {Code}: {Mesage}", titleResult.Error.Code, titleResult.Error.Message);
            return Result<ObjectiveDto>.Failure(null, titleResult.Error);
        }
        
        var descriptionResult = ObjectiveDescription.BuildName(request.RequestDto.Description);
        
        if (!descriptionResult.IsSuccess)
        {
            _logger.LogError("Error: Invalid description, {Code}: {Mesage}", descriptionResult.Error.Code, descriptionResult.Error.Message);
            return Result<ObjectiveDto>.Failure(null, descriptionResult.Error);
        }
        
        var paymentResult = PaymentName.BuildName(request.RequestDto.Payment.Id);
        
        if (!paymentResult.IsSuccess)
        {
            _logger.LogError("Error: Invalid payment, {Code}: {Mesage}", paymentResult.Error.Code, paymentResult.Error.Message);
            return Result<ObjectiveDto>.Failure(null, paymentResult.Error);
        }
        
        var typeResult = ObjectiveTypeTitle.BuildObjectiveTypeTitle(request.RequestDto.Type.Id);
        
        if (!typeResult.IsSuccess)
        {
            _logger.LogError("Error: Invalid type, {Code}: {Mesage}", typeResult.Error.Code, typeResult.Error.Message);
            return Result<ObjectiveDto>.Failure(null, typeResult.Error);
        }
        
        var creator = await _userRepository.GetByIdAsync(request.RequestDto.Creator.Id, cancellationToken);
        if (creator is null)
        {
            var error = UserErrors.NotFound(request.RequestDto.Creator.Id);
            _logger.LogError("Error: Invalid creator, {Code}: {Mesage}", error.Code, error.Message);
            return Result<ObjectiveDto>.Failure(null, error);
        }
        
        foreach (var category in request.RequestDto.Categories)
        {
            var categoryResult = CategoryName.BuildCategoryName(category.Title);
            if (!categoryResult.IsSuccess)
            {
                _logger.LogError("Error: Invalid category, {Code}: {Mesage}", categoryResult.Error.Code, categoryResult.Error.Message);
                return Result<ObjectiveDto>.Failure(null, categoryResult.Error);
            }
        }
        
        var categoriesCollection = request.RequestDto.Categories.Select(async c =>
        {
            
            var category = _categoryRepository.GetByTitle(c.Title!, cancellationToken);
            if (category is null)
            {
                var createdCategory = await _categoryRepository.CreateAsync(new Category(
                    Guid.NewGuid(),
                    CategoryName.BuildCategoryName(c.Title!).Value!, null!), cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                return createdCategory;
            }
        
            return category;
        });
        
        var categoriesList = (await Task.WhenAll(categoriesCollection)).ToList();
        
        Objective objective = new Objective(
            id: Guid.NewGuid(),
            title: titleResult.Value!,
            description: descriptionResult.Value!,
            payment: null,
            paymentAmount: request.RequestDto.PaymentAmount,
            objectiveStatus: null,
            categories: categoriesList,
            type: null,
            attachments: null,
            paymentId: request.RequestDto.Payment.Id,
            objectiveStatusId: ObjectiveStatusTitleVariations.WaitingForAssignment,
            creatorId: creator.Id,
            creator: null,
            creatorPublicContacts: request.RequestDto.CreatorPublicContacts,
            typeId: request.RequestDto.Type.Id,
            eta: request.RequestDto.Eta);
        
        
        await _objectiveRepository.CreateAsync(objective, cancellationToken); 
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Objective with Id = {id} created successfully", objective.Id);
        return Result<ObjectiveDto>.Success(request.RequestDto);
    }
}