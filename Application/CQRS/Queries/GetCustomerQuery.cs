using Application.Dtos;
using MediatR;

namespace Application.CQRS.Queries
{
    /// <summary>
    /// Query للحصول على بيانات عميل محدد مع أحدث طلباته
    /// يتم تنفيذها بواسطة GetCustomerQueryHandler
    /// </summary>
    public class GetCustomerQuery : IRequest<CustomerWithOrdersDto>
    {
        public Guid CustomerId { get; set; }
    }
}
