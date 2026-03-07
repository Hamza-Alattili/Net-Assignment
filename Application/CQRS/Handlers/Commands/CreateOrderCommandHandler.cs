using Application.CQRS.Commands;
using Application.Repositories.Interface;
using Domain.Entities;
using Domain.Entities.Enum;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
    /// <summary>
    /// Handler لـ CreateOrderCommand
    /// يتولى تنفيذ عملية إنشاء طلب جديد
    /// 
    /// الخطوات:
    /// 1. استقبال الـ Command من Controller
    /// 2. التحقق من صحة البيانات (يتم بواسطة Validator)
    /// 3. التحقق من وجود العميل
    /// 4. إنشاء entity جديد من Order مع توليد TrackingNumber
    /// 5. إضافته إلى قاعدة البيانات
    /// 6. حفظ التغييرات
    /// 7. إرجاع معرف الطلب الجديد
    /// </summary>
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IGenericRepository.IUnitOfWork _unitOfWork;

        public CreateOrderCommandHandler(IGenericRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            // التحقق من وجود العميل
            var customer = await _unitOfWork.Customers.GetByIdAsync(request.CustomerId);
            if (customer == null)
            {
                throw new Exception($"العميل برقم {request.CustomerId} غير موجود");
            }

            // إنشاء entity جديد
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = request.CustomerId,
                TrackingNumber = GenerateTrackingNumber(), // توليد رقم تتبع فريد
                Total = request.Total,
                Status = OrderStatus.Pending, // الحالة الافتراضية للطلب الجديد
                CreatedOn = DateTime.UtcNow
            };

            // إضافة الطلب إلى قاعدة البيانات
            await _unitOfWork.Orders.AddAsync(order);

            // حفظ التغييرات
            await _unitOfWork.CompleteAsync();

            // إرجاع معرف الطلب الجديد
            return order.Id;
        }

        /// <summary>
        /// توليد رقم تتبع فريد للطلب
        /// الصيغة: ORD-{Timestamp}-{RandomNumber}
        /// مثال: ORD-1704067200000-5839
        /// </summary>
        private string GenerateTrackingNumber()
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var random = new Random().Next(1000, 9999);
            return $"ORD-{timestamp}-{random}";
        }
    }
}
