using MediatR;

namespace Application.CQRS.Commands
{
    /// <summary>
    /// Command لإنشاء طلب جديد
    /// يتم تنفيذه بواسطة CreateOrderCommandHandler
    /// </summary>
    public class CreateOrderCommand : IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public decimal Total { get; set; }
    }
}
