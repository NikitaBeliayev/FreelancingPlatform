using Application.Objectives;
using Application.Objectives.CreateObjective;
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
}