using Application.Abstraction.Messaging;
using AutoMapper;
using Domain.Repositories;
using Shared;

namespace Application.Objectives.Categories.GetById
{
    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.GetByIdAsync(query.CategoryId, cancellationToken);

            return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(result));
        }
    }
}
