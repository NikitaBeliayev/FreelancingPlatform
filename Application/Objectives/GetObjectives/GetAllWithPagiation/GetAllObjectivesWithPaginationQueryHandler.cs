using Application.Abstraction.Data;
using Application.Abstraction.Messaging;
using Application.Objectives.Categories;
using Application.Objectives.Types;
using Application.Objectives.Types.GetByIdWithPagination;
using Application.Objectives.Types.ResponseDto;
using Application.Users;
using AutoMapper;
using Domain.Categories;
using Domain.Repositories;
using Domain.Types;
using Domain.Users;
using Microsoft.Extensions.Logging;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Objectives.GetObjectives.GetAllWithPagiation
{
	public class GetAllObjectivesWithPaginationQueryHandler : IQueryHandler<GetAllObjectivesWithPaginationQuery, IEnumerable<ResponseObjectiveDto>>
	{
		private readonly ILogger<GetAllObjectivesWithPaginationQueryHandler> _logger;
		private readonly IMapper _mapper;
		private readonly IObjectiveRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public GetAllObjectivesWithPaginationQueryHandler(ILogger<GetAllObjectivesWithPaginationQueryHandler> logger, IMapper mapper, IObjectiveRepository repository, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			_mapper = mapper;
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<IEnumerable<ResponseObjectiveDto>>> Handle(GetAllObjectivesWithPaginationQuery request, CancellationToken cancellationToken)
		{
			_logger.LogInformation("Get all objective types with pagination has been requested");
			var result = _repository.GetAllForImplementorWithPagination(request.Take, request.Skip, cancellationToken);
			var response = new List<ResponseObjectiveDto>();

			await foreach (var objective in result)
			{
				response.Add(new ResponseObjectiveDto() 
				{ 
					Id = objective.Id, 
					Title = objective.Title.Value, 
					Description = objective.Description.Value,
					PaymentId = objective.PaymentId, 
					PaymentAmount = objective.PaymentAmount,
					Tags = objective.Categories.Select(t => new CategoryDto { Id = t.Id, Title = t.Title.Value }).ToList(),
					CreatorPublicContacts = objective.CreatorPublicContacts,
					Creator = new UserDto { Id = objective.Id, FirstName = objective.Creator.FirstName.Value, LastName = objective.Creator.LastName.Value, Email = objective.Creator.Email.Value },
					Type = new TypeDto { Id = objective.Type.Id, TypeTitle = objective.Type.TypeTitle.Title, Duration = objective.Type.Duration },
					Deadline = objective.Eta,
				});
			}

			return Result<IEnumerable<ResponseObjectiveDto>>.Success(response);
		}
	}
}
