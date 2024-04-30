using Application.Objectives.Categories.CreateByTitle;
using Application.Objectives.Categories.GetById;
using Application.Objectives.Categories.GetByTitle;
using Application.Objectives.Categories.RequestDto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreelancingPlatform.Controllers
{
    [Authorize]
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ISender _sender;
        public TagController(ISender sender)
        {
            _sender = sender;
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var command = new GetCategoryByIdQuery(id);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] CategorySearchDto searchParams, CancellationToken cancellationToken)
        {
            var command = new GetByTitleWithPaginationQuery(searchParams);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
