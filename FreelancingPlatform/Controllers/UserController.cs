using Application.Users;
using Application.Users.Create;
using Application.Users.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreelancingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        public UserController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var command = new GetUserByIdQuery(id);
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess 
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpPost()]
        public async Task<IActionResult> Post([FromBody]UserDto user, CancellationToken cancellationToken)
        {
            var command = new CreateUserCommand(user);
            var result = await _sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
    }
}
