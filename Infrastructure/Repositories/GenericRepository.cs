using Application.Repositories.Interface;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Generic Repository - يوفر عمليات CRUD أساسية لأي Entity
    /// هذا يقلل من تكرار الكود ويجعل العمل مع قاعدة البيانات أسهل
    /// </summary>
    public class GenericRepository<T> : IGenericRepository.IRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// الحصول على entity محدد من خلال معرفه
        /// </summary>
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// الحصول على جميع entities
        /// </summary>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// البحث عن entities بناءً على شرط معين
        /// مثال: FindAsync(c => c.Name == "Ahmed")
        /// </summary>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// إضافة entity جديد
        /// </summary>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        /// <summary>
        /// تحديث entity موجود
        /// </summary>
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        /// <summary>
        /// حذف entity
        /// </summary>
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
