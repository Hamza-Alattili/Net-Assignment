using Application.Dtos;
using Domain.Entities.Enum;
using MediatR;

namespace Application.CQRS.Queries
{
    /// <summary>
    /// Query للحصول على جميع الطلبات المصفاة حسب الحالة
    /// يتم تنفيذها بواسطة GetOrdersByStatusQueryHandler
    /// </summary>
    public class GetOrdersByStatusQuery : IRequest<List<OrderDto>>
    {
        public OrderStatus Status { get; set; }
    }
}
