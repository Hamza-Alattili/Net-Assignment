using Domain.Entities;
using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories.Interface
{
    public class IGenericRepository
    {
        public interface IRepository<T> where T : class
        {
            Task<T> GetByIdAsync(Guid id);
            Task<IEnumerable<T>> GetAllAsync();
            Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
            Task AddAsync(T entity);
            void Update(T entity);
            void Delete(T entity);
        }

        // واجهة مستودع العملاء (Customer Repository Interface)
        // يمكن إضافة عمليات خاصة بالعملاء هنا
        public interface ICustomerRepository : IRepository<Customer>
        {
            Task<Customer> GetCustomerWithOrdersAsync(Guid id);
        }

        // واجهة مستودع الطلبات (Order Repository Interface)
        public interface IOrderRepository : IRepository<Order>
        {
            Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        }

        // واجهة وحدة العمل (Unit of Work Interface)
        // تضمن أن جميع التغييرات على قاعدة البيانات تتم كعملية واحدة (Atomic)
        public interface IUnitOfWork : IDisposable
        {
            ICustomerRepository Customers { get; }
            IOrderRepository Orders { get; }
            Task<int> CompleteAsync(); // لحفظ التغييرات في قاعدة البيانات
        }
    }
}
