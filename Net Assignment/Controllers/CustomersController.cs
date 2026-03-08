using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Net_Assignment.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

      
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            try
            {
                var customerId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetCustomer), new { id = customerId }, customerId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] UpdateCustomerCommand command)
        {
            try
            {
                command.Id = id;
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerWithOrdersDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerWithOrdersDto>> GetCustomer(Guid id)
        {
            try
            {
                var query = new GetCustomerQuery { CustomerId = id };
                var customer = await _mediator.Send(query);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
