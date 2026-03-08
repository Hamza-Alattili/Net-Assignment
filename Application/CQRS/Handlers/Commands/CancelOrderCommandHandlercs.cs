using Application.CQRS.Commands;
using Application.Repositories.Interface;
using Domain.Entities.Enum;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IGenericRepository.IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(IGenericRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                throw new Exception($"الطلب برقم {request.OrderId} غير موجود");
            }

            if (order.Status == OrderStatus.Completed)
            {
                throw new Exception("لا يمكن إلغاء طلب مكتمل بالفعل");
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                throw new Exception("الطلب ملغى بالفعل");
            }

            order.Status = OrderStatus.Cancelled;

            _unitOfWork.Orders.Update(order);

            await _unitOfWork.CompleteAsync();
        }

        Task<bool> IRequestHandler<CancelOrderCommand, bool>.Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
