using Application.Objectives;
using Application.Objectives.CreateObjective;
using Application.Objectives.GetObjectives.GetAllWithPagiation;
using Application.Objectives.Types.GetByIdWithPagination;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancingPlatform.Controllers;

[ApiController]
[Route("api/tasks")]
public class ObjectiveController : ControllerBase
{
    private readonly IMediator _sender;
    public ObjectiveController(IMediator sender)
    {
        _sender = sender;
    }



    [HttpPost]
    //[Authorize(Roles = "Admin,Customer")]
    public async Task<IActionResult> Create([FromBody] ObjectiveDto requestDto, CancellationToken cancellationToken = default)
    {
        var command = new CreateObjectiveCommand(requestDto);
        var result = await _sender.Send(command, cancellationToken);
        return new ObjectResult(result);
    }

	[HttpGet("{pageNum:int}/{pageSize:int}")]
	[Authorize(Roles = "Implementer")]
	public async Task<IActionResult> Get(int pageNum, int pageSize, CancellationToken cancellationToken)
	{
		var command = new GetAllObjectivesWithPaginationQuery(pageSize, (pageNum - 1) * pageSize);
		var result = await _sender.Send(command, cancellationToken);

		return Ok(result);
	}
}