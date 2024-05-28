using Application.Abstraction.Messaging;
using Application.Helpers;
using Application.Models;
using Application.Objectives.Categories.GetByTitle;
using Application.Objectives.GetObjectives.GetAllForCustomer;
using Application.Objectives.ResponseDto;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Objectives;
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

            var categories = await _categoryRepository.GetAllWithIncludesAndPaginationAsync(query.pageSize, query.pageNum, cancellationToken);

            if (!categories.result.Any())
            {
                return ResponseHelper.LogAndReturnError<PaginationModel<CategoryDto>>("No categories found",
                    new Error(typeof(GetAllObjectivesByCreatorCommandHandler).Namespace!, "No categories found", 200));
            }

            var objectiveDtos = categories.result.Select(_mapper.Map<CategoryDto>);
            var result = new PaginationModel<CategoryDto>(categories.count, objectiveDtos, query.pageNum, query.pageSize);

            return Result<PaginationModel<CategoryDto>>.Success(_mapper.Map<PaginationModel<CategoryDto>>(result));
        }
    }
}
