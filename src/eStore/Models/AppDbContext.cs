using eStore.Utils;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace eStore.Models
{

    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Brand>().ForSqlServerToTable(SessionVars.Brands)
                .Property(c => c.Id).UseSqlServerIdentityColumn();
            builder.Entity<Product>().ForSqlServerToTable("Products");
            builder.Entity<Order>().ForSqlServerToTable("Orders")
                .Property(t => t.Id).UseSqlServerIdentityColumn();
            builder.Entity<OrderLineItem>().ForSqlServerToTable("OrderItems")
                .Property(ti => ti.Id).UseSqlServerIdentityColumn();
            builder.Entity<Branch>().ForSqlServerToTable("Branches")
                .Property(s => s.Id).UseSqlServerIdentityColumn();
        }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderLineItem> OrderLineItems { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
    }
}
