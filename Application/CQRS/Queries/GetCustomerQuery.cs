using Application.Dtos;
using MediatR;

namespace Application.CQRS.Queries
{
  
    public class GetCustomerQuery : IRequest<CustomerWithOrdersDto>
    {
        public Guid CustomerId { get; set; }
    }
}
