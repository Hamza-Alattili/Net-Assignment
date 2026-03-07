using Application.CQRS.Queries;
using Application.Dtos;
using Application.Repositories.Interface;
using MediatR;

namespace Application.CQRS.Handlers.Queries
{
    /// <summary>
    /// Handler لـ GetOrdersByStatusQuery
    /// يتولى تنفيذ عملية استرجاع الطلبات المصفاة حسب الحالة
    /// 
    /// الخطوات:
    /// 1. استقبال الـ Query من Controller
    /// 2. البحث عن جميع الطلبات بالحالة المحددة
    /// 3. تحويل البيانات إلى DTO
    /// 4. إرجاع قائمة الطلبات
    /// </summary>
    public class GetOrdersByStatusQueryHandler : IRequestHandler<GetOrdersByStatusQuery, List<OrderDto>>
    {
        private readonly IGenericRepository.IUnitOfWork _unitOfWork;

        public GetOrdersByStatusQueryHandler(IGenericRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersByStatusQuery request, CancellationToken cancellationToken)
        {
            // البحث عن جميع الطلبات بالحالة المحددة
            var orders = await _unitOfWork.Orders.GetOrdersByStatusAsync(request.Status);

            // تحويل البيانات إلى DTO
            var orderDtos = orders.Select(o => new OrderDto
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                TrackingNumber = o.TrackingNumber,
                Total = o.Total,
                Status = o.Status,
                CreatedOn = o.CreatedOn
            }).ToList();

            return orderDtos;
        }
    }
}
