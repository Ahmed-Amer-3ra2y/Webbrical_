using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.DTOs
{
    public class MenueItemDto
    {
        public int? ItemID { get; set; }
        [Required( ErrorMessage = "Item Name is Required" )]
        [MaxLength( 50 , ErrorMessage = "Name must be less than 50 character" )]
        public string Name { get; set; } = string.Empty;
        [Required( ErrorMessage = "price is Required" )]
        public int price { get; set; }
        public byte[]? image { get; set; }
        [Required( ErrorMessage = " Description is Required" )]
        public string Description { get; set; } = string.Empty;
        [Required( ErrorMessage = " size is Required" )]
        public string size { get; set; } = string.Empty;
        public bool ?IsAccepted { get; set; }
        public bool ?IsTopItem { get; set; }

        public int ResturantID { get; set; }

        public bool ?Offer { get; set; }
        public int CategoryID { get; set; }
        public string? CName { get; set; }=string.Empty;
       public IFormFile ?PhotoFile { get; set; }

    }
}
