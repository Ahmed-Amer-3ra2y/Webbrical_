using ECommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Configurations
{
    public class OrderEntityTypeCofiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(key => key.orderID);
            builder.HasOne(R => R.item).WithMany(O => O.Orders).HasForeignKey(R => R.itemID);
            //builder.HasOne(c => c.item).WithMany(U => U.users).HasForeignKey(c => c.userID);



        }
    }
}
