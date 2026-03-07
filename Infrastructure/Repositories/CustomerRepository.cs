using Application.Repositories.Interface;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Customer Repository - يحتوي على عمليات خاصة بالعملاء
    /// يرث من GenericRepository ويضيف عمليات إضافية مخصصة
    /// </summary>
    public class CustomerRepository : GenericRepository<Customer>, IGenericRepository.ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// الحصول على عميل مع جميع طلباته
        /// يستخدم Include لتحميل الطلبات بنفس الاستعلام (Eager Loading)
        /// </summary>
        public async Task<Customer> GetCustomerWithOrdersAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
