using Application.CQRS.Commands;
using Application.Repositories.Interface;
using Domain.Entities.Enum;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    /// <summary>
    /// Handler لـ CancelOrderCommand
    /// يتولى تنفيذ عملية إلغاء طلب موجود
    /// 
    /// الخطوات:
    /// 1. استقبال الـ Command من Controller
    /// 2. التحقق من صحة البيانات (يتم بواسطة Validator)
    /// 3. البحث عن الطلب في قاعدة البيانات
    /// 4. التحقق من أن الطلب قابل للإلغاء (ليس مكتمل بالفعل)
    /// 5. تحديث حالة الطلب إلى Cancelled
    /// 6. حفظ التغييرات
    /// </summary>
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand>
    {
        private readonly IGenericRepository.IUnitOfWork _unitOfWork;

        public CancelOrderCommandHandler(IGenericRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            // البحث عن الطلب
            var order = await _unitOfWork.Orders.GetByIdAsync(request.OrderId);

            if (order == null)
            {
                throw new Exception($"الطلب برقم {request.OrderId} غير موجود");
            }

            // التحقق من أن الطلب قابل للإلغاء
            if (order.Status == OrderStatus.Completed)
            {
                throw new Exception("لا يمكن إلغاء طلب مكتمل بالفعل");
            }

            if (order.Status == OrderStatus.Cancelled)
            {
                throw new Exception("الطلب ملغى بالفعل");
            }

            // تحديث حالة الطلب
            order.Status = OrderStatus.Cancelled;

            // تحديث الطلب في قاعدة البيانات
            _unitOfWork.Orders.Update(order);

            // حفظ التغييرات
            await _unitOfWork.CompleteAsync();
        }
    }
}
