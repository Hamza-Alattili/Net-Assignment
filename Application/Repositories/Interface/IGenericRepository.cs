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

        
        public interface ICustomerRepository : IRepository<Customer>
        {
            Task<Customer> GetCustomerWithOrdersAsync(Guid id);
        }

        public interface IOrderRepository : IRepository<Order>
        {
            Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status);
        }

   
        public interface IUnitOfWork : IDisposable
        {
            ICustomerRepository Customers { get; }
            IOrderRepository Orders { get; }
            Task<int> CompleteAsync(); 
        }
    }
}
