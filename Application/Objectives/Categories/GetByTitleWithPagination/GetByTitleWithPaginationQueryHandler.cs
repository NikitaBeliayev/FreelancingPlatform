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
            string search = string.IsNullOrWhiteSpace(query.SearchParams.search) ? "" : query.SearchParams.search;
            var result = _categoryRepository.GetByTitleWithPagination(category => category.Title.Value.Contains(search), 
                query.SearchParams.pageSize, query.SearchParams.skip, cancellationToken);

            return Result<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(result));
        }
    }
}
