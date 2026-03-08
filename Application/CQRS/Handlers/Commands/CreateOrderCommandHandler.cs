using Application.CQRS.Commands;
using Application.Repositories.Interface;
using Domain.Entities;
using Domain.Entities.Enum;
using MediatR;

namespace Application.CQRS.Handlers.Commands
{
   
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
                TrackingNumber = GenerateTrackingNumber(),
                Total = request.Total,
                Status = OrderStatus.Pending, 
                CreatedOn = DateTime.UtcNow
            };

            await _unitOfWork.Orders.AddAsync(order);

            await _unitOfWork.CompleteAsync();

            return order.Id;
        }

       
        private string GenerateTrackingNumber()
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var random = new Random().Next(1000, 9999);
            return $"ORD-{timestamp}-{random}";
        }
    }
}
