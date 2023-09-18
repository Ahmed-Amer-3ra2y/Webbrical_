using ECommerce.DAL.Configurations;
using ECommerce.DAL.Models;
using ECommerce.DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerce
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base( options )
        {

        }

        protected override void OnModelCreating( ModelBuilder model )
        {
            base.OnModelCreating( model );
            model.ApplyConfigurationsFromAssembly( typeof( RestaurantEntityTypeConfiguration ).Assembly );
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Resturant> Restaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Review> Reviews { get; set; }
        // public DbSet<RefreshToken> RefreshTokens { get; set; }

    }
}
