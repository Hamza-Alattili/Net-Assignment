using Application.Repositories.Interface;
using Domain.Entities;
using Domain.Entities.Enum;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    
    public class OrderRepository : GenericRepository<Order>, IGenericRepository.IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

       
        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _dbSet
                .Where(o => o.Status == status)
                .ToListAsync();
        }
    }
}
