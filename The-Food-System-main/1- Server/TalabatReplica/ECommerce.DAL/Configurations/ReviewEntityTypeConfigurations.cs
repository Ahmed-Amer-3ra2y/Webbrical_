using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.DAL.Configurations
{
    public class ReviewEntityTypeConfigurations : IEntityTypeConfiguration<Review>
    {
        public void Configure( EntityTypeBuilder<Review> builder )
        {
            builder.HasKey( prop => prop.ID );
            builder.Property( prop => prop.CustomerID ).HasMaxLength( 450 );
            builder.Property( prop => prop.Comment ).HasMaxLength( 200 );
            builder.HasOne( prop => prop.Restaurant ).WithMany( prop => prop.Reviews ).HasForeignKey( prop => prop.ResID ).OnDelete( DeleteBehavior.NoAction );
            builder.HasOne( prop => prop.Customer ).WithMany( prop => prop.Reviews ).HasForeignKey( prop => prop.CustomerID ).OnDelete( DeleteBehavior.NoAction );
        }
    }
}
