using Application.Repositories.Interface;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    
    public class CustomerRepository : GenericRepository<Customer>, IGenericRepository.ICustomerRepository
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }

        
        public async Task<Customer> GetCustomerWithOrdersAsync(Guid id)
        {
            return await _dbSet
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
