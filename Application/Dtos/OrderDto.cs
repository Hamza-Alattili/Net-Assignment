using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string TrackingNumber { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
