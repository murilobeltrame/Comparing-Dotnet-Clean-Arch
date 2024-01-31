using CleanArchByMe.Application.TodoUseCases.Commands;
using CleanArchByMe.Application.TodoUseCases.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchByMe.Api.TodosApi
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> Fetch([FromQuery] FetchTodosQuery query) => Ok(await _mediator.Send(query));

        // GET api/<TodosController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id) => Ok(await _mediator.Send(new GetTodoByIdQuery(id)));

        // POST api/<TodosController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTodoCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        // PUT api/<TodosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateTodoCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<TodosController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTodoCommand(id));
            return NoContent();
        }
    }
}
