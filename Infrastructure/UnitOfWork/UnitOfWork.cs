using Application.Repositories.Interface;
using Infrastructure.Context;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    /// <summary>
    /// Unit of Work Pattern - يضمن أن جميع التغييرات على قاعدة البيانات تتم كعملية واحدة (Atomic)
    /// إذا حدث خطأ في أي عملية، يتم التراجع عن جميع التغييرات
    /// 
    /// الفائدة:
    /// - ضمان تكامل البيانات (Data Integrity)
    /// - إدارة مركزية للمستودعات
    /// - تسهيل الاختبار (Testing)
    /// </summary>
    public class UnitOfWork : IGenericRepository.IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository.ICustomerRepository _customerRepository;
        private IGenericRepository.IOrderRepository _orderRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// الوصول إلى Customer Repository
        /// يتم إنشاء instance واحد فقط (Lazy Loading)
        /// </summary>
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

        /// <summary>
        /// الوصول إلى Order Repository
        /// يتم إنشاء instance واحد فقط (Lazy Loading)
        /// </summary>
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

        /// <summary>
        /// حفظ جميع التغييرات في قاعدة البيانات
        /// يتم تنفيذ جميع العمليات كـ Transaction واحد
        /// </summary>
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// تحرير الموارد
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
