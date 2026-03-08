using MediatR;

namespace Application.CQRS.Commands
{
    /// <summary>
    /// Command لإلغاء طلب موجود
    /// يتم تنفيذه بواسطة CancelOrderCommandHandler
    /// </summary>
    public class CancelOrderCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
    }
}
