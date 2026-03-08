using Application.Repositories.Interface;
using Infrastructure.Context;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
  
    public class UnitOfWork : IGenericRepository.IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository.ICustomerRepository _customerRepository;
        private IGenericRepository.IOrderRepository _orderRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IGenericRepository.ICustomerRepository Customers
        {
            get
            {
                if (_customerRepository == null)
                {
                    _customerRepository = new CustomerRepository(_context);
                }
                return _customerRepository;
            }
        }

        
        public IGenericRepository.IOrderRepository Orders
        {
            get
            {
                if (_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_context);
                }
                return _orderRepository;
            }
        }

       
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

       
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
