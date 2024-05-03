using System.ComponentModel.DataAnnotations;
using Application.Objectives.Categories.GetByTitle;
using Application.Objectives.Types;
using Application.Objectives.Types.Create;
using Application.Objectives.Types.Delete;
using Application.Objectives.Types.GetById;
using Application.Objectives.Types.GetByIdWithPagination;
using Application.Objectives.Types.RequestDto;
using Application.Objectives.Types.Update;
using Domain.Roles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancingPlatform.Controllers
{
    [Authorize]
    [Route("api/types")]
    [ApiController]
    public class ObjectiveTypeController : ControllerBase
    {
        private readonly ISender _sender;
        public ObjectiveTypeController(ISender sender)
        {
            _sender = sender;
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var command = new GetByIdQuery(id);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNum, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var command = new GetAllObjectiveTypesWithPaginationQuery(pageNum, pageSize);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }
        
        [Authorize(Roles = "Admin")] // change this to variable in the future
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TypeDto requestDto, CancellationToken cancellationToken)
        {
            var command = new CreateObjectiveTypeCommand(requestDto);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }
        
        [Authorize(Roles = "Admin")] // change this to variable in the future
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteObjectiveTypeCommand(id);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }
        
        [Authorize(Roles = "Admin")] // change this to variable in the future
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateObjectiveTypeRequestDto requestDto, CancellationToken cancellationToken)
        {
            var command = new UpdateObjectiveTypeCommand(requestDto);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}