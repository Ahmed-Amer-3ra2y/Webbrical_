using ECommerce.DAL.Models;
using ECommerce.DAL.Models.IdentityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BAL.DTOs
{
    public class orderDto
    {
        public int orderID { get; set; }
        public int itemID { get; set; }
      //  public MenuItem item { get; set; }
       public string UserID { get; set; } = string.Empty;
     //   public ApplicationUser users { get; set; }
       public int totalPrice { get; set; }
    }
}
