using MediatR;

namespace Application.CQRS.Commands
{
    
    public class CancelOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
    }
}
