using Application.Objectives.Categories.CreateByTitle;
using Application.Objectives.Categories.GetById;
using Application.Objectives.Categories.GetByTitle;
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

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] int pageNum, [FromQuery] int pageSize, CancellationToken cancellationToken)
        {
            var command = new GetByTitleWithPaginationQuery(pageNum, pageSize);
            var result = await _sender.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
