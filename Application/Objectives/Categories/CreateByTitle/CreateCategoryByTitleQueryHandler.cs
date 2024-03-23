using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Helpers;
using AutoMapper;
using Domain.Categories;
using Domain.Repositories;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Categories.CreateByTitle
{
    public class CreateCategoryByTitleQueryHandler : IQueryHandler<CreateCategoryByTitleQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCategoryByTitleQueryHandler> _logger;

        public CreateCategoryByTitleQueryHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateCategoryByTitleQueryHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CategoryDto>> Handle(CreateCategoryByTitleQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Objective category creation has been requested");

            var title = CategoryName.BuildCategoryName(query.title);
            if (!title.IsSuccess)
            {
                return ResponseHelper.LogAndReturnError<CategoryDto>("Invalid category title", title.Error);
            }

            Category category = new Category(title.Value, []);

            var result = await _categoryRepository.CreateByTitleAsync(category, cancellationToken);
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
