using Application.Dtos;
using Domain.Entities.Enum;
using MediatR;

namespace Application.CQRS.Queries
{
    
    public class GetOrdersByStatusQuery : IRequest<List<OrderDto>>
    {
        public OrderStatus Status { get; set; }
    }
}
