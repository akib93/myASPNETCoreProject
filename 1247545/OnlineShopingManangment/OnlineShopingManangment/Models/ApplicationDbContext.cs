using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopingManangment.Models;
using OnlineShopingManangment.Models.AccountViewModels;

namespace OnlineShopingManangment.Models
{
    public class ApplicationDbContext:IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems { get; set; }
        //public DbSet<ShoppingCart> shoppingCarts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        internal void CreateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public DbSet<OnlineShopingManangment.Models.AccountViewModels.RegisterViewModel> RegisterViewModel { get; set; }

        public DbSet<OnlineShopingManangment.Models.AccountViewModels.LoginViewModel> LoginViewModel { get; set; }

    }
}
