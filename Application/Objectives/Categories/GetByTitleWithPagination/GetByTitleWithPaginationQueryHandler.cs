using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Objectives.Categories.GetByTitle;
using Application.Objectives.Categories.ResponseDto;
using Application.Objectives.Types.GetByIdWithPagination;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Categories.GetCategoryByTitleWithPagination
{
    public class GetByTitleWithPaginationQueryHandler : IQueryHandler<GetByTitleWithPaginationQuery, PaginationModel<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByTitleWithPaginationQuery> _logger;

        public GetByTitleWithPaginationQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<GetByTitleWithPaginationQuery> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<PaginationModel<CategoryDto>>> Handle(GetByTitleWithPaginationQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get multiple categories requested");

            if (query.pageNum <= 0)
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<CategoryDto>>("The page number must be greater than 0",
                    new Error(typeof(GetByTitleWithPaginationQueryHandler).Namespace!, "The page number must be greater than 0", 400));
            }

            var (categories, total) = await _categoryRepository.GetByTitleWithPagination(query.pageSize, (query.pageNum - 1) * query.pageSize, cancellationToken);

            var response = new List<CategoryDto>();

            await foreach (var category in categories)
            {
                response.Add(new CategoryDto() { Id = category.Id, Title = category.Title.Value });
            }

            var result = new PaginationModel<CategoryDto>(total, response, query.pageNum, query.pageSize);

            return Result<PaginationModel<CategoryDto>>.Success(_mapper.Map<PaginationModel<CategoryDto>>(result));
        }
    }
}
