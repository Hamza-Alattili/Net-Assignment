using Application.CQRS.Queries;
using Application.Dtos;
using Application.Repositories.Interface;
using MediatR;

namespace Application.CQRS.Handlers.Queries
{
    
    public class GetOrdersByStatusQueryHandler : IRequestHandler<GetOrdersByStatusQuery, List<OrderDto>>
    {
        private readonly IGenericRepository.IUnitOfWork _unitOfWork;

        public GetOrdersByStatusQueryHandler(IGenericRepository.IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<OrderDto>> Handle(GetOrdersByStatusQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetOrdersByStatusAsync(request.Status);

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
