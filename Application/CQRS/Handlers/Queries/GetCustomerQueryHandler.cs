using Application.CQRS.Queries;
using Application.Dtos;
using Application.Repositories.Interface;
using MediatR;

namespace Application.CQRS.Handlers.Queries
{
    /// <summary>
    /// Handler لـ GetCustomerQuery
    /// يتولى تنفيذ عملية استرجاع بيانات عميل مع أحدث طلباته
    /// 
    /// الخطوات:
    /// 1. استقبال الـ Query من Controller
    /// 2. البحث عن العميل مع طلباته
    /// 3. تحويل البيانات إلى DTO
    /// 4. إرجاع البيانات
    /// </summary>
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerWithOrdersDto>
    {
        private readonly IGenericRepository.IUnitOfWork _unitOfWork;

        public GetCustomerQueryHandler(IGenericRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomerWithOrdersDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            // البحث عن العميل مع طلباته
            var customer = await _unitOfWork.Customers.GetCustomerWithOrdersAsync(request.CustomerId);

            if (customer == null)
            {
                throw new Exception($"العميل برقم {request.CustomerId} غير موجود");
            }

            // تحويل البيانات إلى DTO
            var customerDto = new CustomerWithOrdersDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                CreatedOn = customer.CreatedOn,
                Orders = customer.Orders.Select(o => new OrderDto
                {
                    Id = o.Id,
                    CustomerId = o.CustomerId,
                    TrackingNumber = o.TrackingNumber,
                    Total = o.Total,
                    Status = o.Status,
                    CreatedOn = o.CreatedOn
                }).ToList()
            };

            return customerDto;
        }
    }
}
