using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BAL.DTOs
{
    public class CategoryItemDto
    {
        public int? CategoryID { get; set; }
        public List<MenueItemDto> MenuItems { get; set; } 
    }
}
