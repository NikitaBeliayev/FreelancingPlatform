using Application.Objectives;
using Application.Objectives.CreateObjective;
using Application.Objectives.GetObjective;
using Application.Objectives.GetObjectives.GetAllForCustomer;
using Application.Objectives.GetObjectives.GetAllWithPagiation;
using Application.Objectives.GetObjectives.GetAssignedTasksForImplementor;
using Application.Objectives.RequestDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace FreelancingPlatform.Controllers;

[ApiController]
[Route("api/tasks")]
public class TaskController : ControllerBase
{
    private readonly IMediator _sender;
    public TaskController(IMediator sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Customer")]
    public async Task<IActionResult> Create([FromBody] ObjectiveCreateDto requestDto, CancellationToken cancellationToken = default)
    {
        var command = new CreateObjectiveCommand(requestDto, Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        var result = await _sender.Send(command, cancellationToken);
        return new ObjectResult(result);
    }

	[HttpGet]
    [Authorize(Roles = "Implementer")]
	public async Task<IActionResult> GetAll([FromQuery] int pageNum, [FromQuery] int pageSize, CancellationToken cancellationToken)
	{
		var command = new GetAllObjectivesWithPaginationQuery(pageNum, pageSize);
		var result = await _sender.Send(command, cancellationToken);

		return Ok(result);
	}

    [HttpGet("/api/creators/tasks")]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Get([FromQuery]int pageNum, [FromQuery]int pageSize, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId);

        var command = new GetAllObjectivesByCreatorQuery(id, pageNum, pageSize);
        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet("/api/implementors/tasks")]
    [Authorize(Roles = "Implementer")]
    public async Task<IActionResult> GetAssigned([FromQuery] int pageNum, [FromQuery] int pageSize, CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var id = Guid.Parse(userId);

        var command = new GetAssignedTasksForImplementorQuery(id, pageNum, pageSize);
        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpGet("/api/tasks/{id}")]
    [Authorize(Roles = "Implementer,Customer")]
    public async Task<IActionResult> GetTask(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var userRole = User.FindFirstValue(ClaimTypes.Role);

        var command = new GetObjectiveQuery(id, userId, userRole);
        var result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }
}