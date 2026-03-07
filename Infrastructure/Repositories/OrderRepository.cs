using Application.Repositories.Interface;
using Domain.Entities;
using Domain.Entities.Enum;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Order Repository - يحتوي على عمليات خاصة بالطلبات
    /// يرث من GenericRepository ويضيف عمليات إضافية مخصصة
    /// </summary>
    public class OrderRepository : GenericRepository<Order>, IGenericRepository.IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// الحصول على جميع الطلبات بحالة معينة
        /// مثال: GetOrdersByStatusAsync(OrderStatus.Pending)
        /// </summary>
        public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            return await _dbSet
                .Where(o => o.Status == status)
                .ToListAsync();
        }
    }
}
