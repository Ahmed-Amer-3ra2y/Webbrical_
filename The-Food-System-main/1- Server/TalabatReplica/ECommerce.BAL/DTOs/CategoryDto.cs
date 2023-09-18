using ECommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.BAL.DTOs
{
    public class CategoryDto
    {
        
        public int? CategoryID { get; set; }
        [Required(ErrorMessage ="Category Name is Required")]
        [MaxLength(50,ErrorMessage ="Name must be less than 50 character")]
        public string Name { get; set; } = string.Empty;
        
    }
}
