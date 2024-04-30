using Application.Abstraction.Messaging;
using Application.Objectives.Types.ResponseDto;
using AutoMapper;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared;

namespace Application.Objectives.Categories.GetById
{
    public class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper, ILogger<GetCategoryByIdQueryHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get category request has been received for type with Id = {CategoryId}", query.CategoryId);
            var result = await _categoryRepository.GetByIdAsync(query.CategoryId, cancellationToken);

            if (result is not null)
            {
                _logger.LogInformation("Category with Id = {CategoryId} successfully retrieved from the DB", query.CategoryId);
            }
            else
            {
                _logger.LogInformation("There are no category with Id = {CategoryId}", query.CategoryId);
            }

            return Result<CategoryDto>.Success(_mapper.Map<CategoryDto>(result));
        }
    }
}
