using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using AutoMapper;
using Domain.Categories;
using Domain.Objectives;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Categories.CreateByTitle
{
    public class CreateCategoryCommandHandler : IQueryHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryCommandHandler> _logger;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCategoryCommandHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Objective category creation has been requested");

            var title = CategoryName.BuildCategoryName(query.title);
            if (!title.IsSuccess)
            {
                return ResponseHelper.LogAndReturnError<CategoryDto>("Invalid category title", title.Error);
            }

            Category category = new Category(Guid.NewGuid(), title.Value!, new List<Objective>());

            var result = await _categoryRepository.CreateAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (result != null)
            {
                _logger.LogInformation("Objective category created successfully: Id = {CategoryId}", result.Id);
                return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(result));
            }

            return ResponseHelper.LogAndReturnError<CategoryDto>("Objective category creation failed, something wrong", new Error("Objective category.CreateCategoryByTitleQueryHandler", "Something wrong", 500));
        }
    }
}
