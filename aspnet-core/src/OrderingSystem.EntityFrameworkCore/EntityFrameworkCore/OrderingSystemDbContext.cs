using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using OrderingSystem.Authorization.Roles;
using OrderingSystem.Authorization.Users;
using OrderingSystem.MultiTenancy;
using OrderingSystem.OrderSystem.Item;
using OrderingSystem.OrderSystem.Category;
using OrderingSystem.OrderSystem.Discount;

namespace OrderingSystem.EntityFrameworkCore
{
    public class OrderingSystemDbContext : AbpZeroDbContext<Tenant, Role, User, OrderingSystemDbContext>
    {
        public virtual DbSet<OdsItem> OdsItems { get; set; }
        public virtual DbSet<OdsCategory> OdsCategorys { get; set; }
        public virtual DbSet<OdsItemCategory> OdsItemCategory { get; set; }
        public virtual DbSet<OdsDiscount> OdsDiscount { get; set; }

        public OrderingSystemDbContext(DbContextOptions<OrderingSystemDbContext> options)
            : base(options)
        {
        }
    }
}
