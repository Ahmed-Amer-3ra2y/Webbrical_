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
    public class MenuItemtEntityTypeCofiguration : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(key => key.ItemID);
            builder.Property(prop => prop.Name).HasMaxLength(50);
            builder.HasOne(item=>item.resturant).WithMany(res=>res.MenuItems).HasForeignKey(item=>item.ResturantID);
            builder.HasOne(c => c.category).WithMany(m=>m.MenuItems).HasForeignKey(c => c.CategoryID);




        }

       
    }
}
