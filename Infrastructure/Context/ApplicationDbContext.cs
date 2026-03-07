using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // تحديد دقة السعر (Total) في جدول الطلبات
            // 18 خانة إجمالية، منها 2 بعد الفاصلة (مثلاً: 150.50)
            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18,2)");

            // الكود القديم الخاص بالعلاقات (اتركه كما هو)
            var relationShips = modelBuilder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys());

            foreach (var relationship in relationShips)
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }
        }
    }
}