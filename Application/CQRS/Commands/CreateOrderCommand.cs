using MediatR;

namespace Application.CQRS.Commands
{
    
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
    }
}
