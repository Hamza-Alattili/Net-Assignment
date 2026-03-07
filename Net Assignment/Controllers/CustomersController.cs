using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Net_Assignment.Controllers
{
    /// <summary>
    /// Customers Controller
    /// يتولى جميع العمليات المتعلقة بالعملاء
    /// 
    /// الـ Endpoints:
    /// POST /api/customers - إنشاء عميل جديد
    /// PUT /api/customers/{id} - تحديث بيانات عميل
    /// GET /api/customers/{id} - الحصول على بيانات عميل مع طلباته
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// إنشاء عميل جديد
        /// </summary>
        /// <param name="command">بيانات العميل الجديد</param>
        /// <returns>معرف العميل الجديد</returns>
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

        /// <summary>
        /// تحديث بيانات عميل موجود
        /// </summary>
        /// <param name="id">معرف العميل</param>
        /// <param name="command">البيانات المحدثة</param>
        /// <returns>لا يوجد محتوى</returns>
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

        /// <summary>
        /// الحصول على بيانات عميل مع أحدث طلباته
        /// </summary>
        /// <param name="id">معرف العميل</param>
        /// <returns>بيانات العميل مع الطلبات</returns>
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
