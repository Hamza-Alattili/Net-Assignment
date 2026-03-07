using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.Dtos;
using Domain.Entities.Enum;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Net_Assignment.Controllers
{
    /// <summary>
    /// Orders Controller
    /// يتولى جميع العمليات المتعلقة بالطلبات
    /// 
    /// الـ Endpoints:
    /// POST /api/orders - إنشاء طلب جديد
    /// DELETE /api/orders/{id}/cancel - إلغاء طلب
    /// GET /api/orders/status/{status} - الحصول على طلبات بحالة معينة
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// إنشاء طلب جديد
        /// </summary>
        /// <param name="command">بيانات الطلب الجديد</param>
        /// <returns>معرف الطلب الجديد</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            try
            {
                var orderId = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetOrdersByStatus), new { status = OrderStatus.Pending }, orderId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// إلغاء طلب موجود
        /// </summary>
        /// <param name="id">معرف الطلب</param>
        /// <returns>لا يوجد محتوى</returns>
        [HttpDelete("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            try
            {
                var command = new CancelOrderCommand { OrderId = id };
                await _mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// <summary>
        /// الحصول على جميع الطلبات بحالة معينة
        /// </summary>
        /// <param name="status">حالة الطلب (Pending, Completed, Cancelled)</param>
        /// <returns>قائمة الطلبات</returns>
        [HttpGet("status/{status}")]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<OrderDto>>> GetOrdersByStatus(OrderStatus status)
        {
            try
            {
                var query = new GetOrdersByStatusQuery { Status = status };
                var orders = await _mediator.Send(query);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
