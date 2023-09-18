using ECommerce.DAL.Models.IdentityModels;
using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.DTOs
{
    public class CartDto
    {
        public int CartID { get; set; }
        public int itemID { get; set; }
       
        public string UserID { get; set; }

        [Required]
        public string Quantity { get; set; } = string.Empty;
        [Required]
        public int Price { get; set; }
    }
}
