using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Objectives.RequestDto;
using Application.Objectives.ResponseDto;
using AutoMapper;
using Domain.Categories;
using Domain.Categories.Errors;
using Domain.Objectives;
using Domain.Objectives.Errors;
using Domain.Payments;
using Domain.Repositories;
using Domain.Statuses;
using Domain.Types;
using Domain.Users.Errors;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.CreateObjective;

public class CreateObjectiveCommandHandler : ICommandHandler<CreateObjectiveCommand, SimpleResponseObjectiveDto>
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
   
    
    public async Task<Result<SimpleResponseObjectiveDto>> Handle(CreateObjectiveCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Objective creation requested");

        var titleResult = ObjectiveTitle.BuildName(request.RequestDto.Title);
        if (!titleResult.IsSuccess)
        {
            _logger.LogError("Error: Invalid title, {Code}: {Mesage}", titleResult.Error.Code, titleResult.Error.Message);
            return Result<SimpleResponseObjectiveDto>.Failure(null, titleResult.Error);
        }
        
        var descriptionResult = ObjectiveDescription.BuildName(request.RequestDto.Description);
        
        if (!descriptionResult.IsSuccess)
        {
            _logger.LogError("Error: Invalid description, {Code}: {Mesage}", descriptionResult.Error.Code, descriptionResult.Error.Message);
            return Result<SimpleResponseObjectiveDto>.Failure(null, descriptionResult.Error);
        }
        
        //var paymentResult = PaymentName.BuildName(request.RequestDto.Payment.Id);
        
        //if (!paymentResult.IsSuccess)
        //{
        //    _logger.LogError("Error: Invalid payment, {Code}: {Mesage}", paymentResult.Error.Code, paymentResult.Error.Message);
        //    return Result<ResponseObjectiveDto>.Failure(null, paymentResult.Error);
        //}
        
        var typeResult = ObjectiveTypeTitle.BuildObjectiveTypeTitle(request.RequestDto.Type.Id);
        
        if (!typeResult.IsSuccess)
        {
            _logger.LogError("Error: Invalid type, {Code}: {Mesage}", typeResult.Error.Code, typeResult.Error.Message);
            return Result<SimpleResponseObjectiveDto>.Failure(null, typeResult.Error);
        }

        var objectiveDeadlineResult = ObjectiveDeadline.BuildName(request.RequestDto.Deadline);
        if (!objectiveDeadlineResult.IsSuccess)
        {
	        return Result<SimpleResponseObjectiveDto>.Failure(null, objectiveDeadlineResult.Error);
        }
        
        var creator = await _userRepository.GetByIdAsync(request.CreatorId, cancellationToken);
        if (creator is null)
        {
            var error = UserErrors.NotFound(request.CreatorId);
            _logger.LogError("Error: Invalid creator, {Code}: {Mesage}", error.Code, error.Message);
            return Result<SimpleResponseObjectiveDto>.Failure(null, error);
        }

		// logic with new categories creation
		//foreach (var category in request.RequestDto.Categories)
		//{
		//	var categoryResult = CategoryName.BuildCategoryName(category.Title);
		//	if (!categoryResult.IsSuccess)
		//	{
		//		_logger.LogError("Error: Invalid category, {Code}: {Mesage}", categoryResult.Error.Code, categoryResult.Error.Message);
		//		return Result<ResponseObjectiveDto>.Failure(null, categoryResult.Error);
		//	}
		//}

		//var categoriesCollection = request.RequestDto.Categories.Select(async c =>
		//{

		//	var category = _categoryRepository.GetByTitle(c.Title!, cancellationToken);
		//	if (category is null)
		//	{
		//		var createdCategory = await _categoryRepository.CreateAsync(new Category(
		//			Guid.NewGuid(),
		//			CategoryName.BuildCategoryName(c.Title!).Value!, null!), cancellationToken);
		//		await _unitOfWork.SaveChangesAsync(cancellationToken);
		//		return createdCategory;
		//	}

		//	return category;
		//});

		//var categoriesList = (await Task.WhenAll(categoriesCollection)).ToList();

		List<Category> categoriesList = new List<Category>();

        if (request.RequestDto.Tags.Count == 0)
        {
            var error = ObjectiveCategoryErrors.NoTagsProvided;
            _logger.LogError("Error: No tags provided, {Code}: {Message}", error.Code, error.Message);
            return Result<SimpleResponseObjectiveDto>.Failure(null, error);
        }

		foreach (var tag in request.RequestDto.Tags)
		{
			var category = await _categoryRepository.GetByIdAsync(tag.Id, cancellationToken);
			if (category is null)
			{
				var error = CategoryErrors.NotFound(tag.Id);
				_logger.LogError("Error: Invalid category, {Code}: {Mesage}", error.Code, error.Message);
				return Result<SimpleResponseObjectiveDto>.Failure(null, error);
			}

			categoriesList.Add(category);
		}
        
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
            paymentId: new Guid("9abd45ff-4c02-1661-9a54-2316bd7b3b1a"), // hardcoded "coin" value (is not used by far)
            objectiveStatusId: ObjectiveStatusTitleVariations.WaitingForAssignment,
            creatorId: creator.Id,
            creator: null,
            creatorPublicContacts: request.RequestDto.CreatorPublicContacts,
            typeId: request.RequestDto.Type.Id,
            deadline: objectiveDeadlineResult.Value);
        
        
        await _objectiveRepository.CreateAsync(objective, cancellationToken); 
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Objective with Id = {id} created successfully", objective.Id);
        return Result<SimpleResponseObjectiveDto>.Success(new SimpleResponseObjectiveDto { Id = objective.Id });
    }
}