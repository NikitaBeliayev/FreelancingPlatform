using Application.Abstraction.Messaging;
using Application.Objectives.Categories.GetByTitle;
using AutoMapper;
using Domain.Repositories;
using Shared;

namespace Application.Objectives.Categories.GetCategoryByTitleWithPagination
{
    public class GetByTitleWithPaginationQueryHandler : IQueryHandler<GetByTitleWithPaginationQuery, List<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetByTitleWithPaginationQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<CategoryDto>>> Handle(GetByTitleWithPaginationQuery query, CancellationToken cancellationToken)
        {
            var result = _categoryRepository.GetByTitleWithPagination(query.SearchParams.search, query.SearchParams.pageSize, query.SearchParams.skip, cancellationToken);

            return result != null
                ? Result<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(result))
                : Result<List<CategoryDto>>.Success(null);
        }
    }
}
