using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace ECommerce.BAL.DTOs
{
    public class RestaurantDto
    {
        public int? RestaurantID { get; set; }

        [Required( ErrorMessage = "Location Field is required" )]
        //[DataType( DataType.Url , ErrorMessage = "Please Location URL of your restaurant from google maps" )]
        public string Location { get; set; } = string.Empty;

        [Required( ErrorMessage = "Restaurant location Field is required" )]
        [MaxLength( 50 , ErrorMessage = "Exceeded max length of Restaurant name of 50 char" )]
        public string Name { get; set; } = string.Empty;
        public byte[ ]?Poster { get; set; }
        public byte[ ]? CoverBanner { get; set; }
        [Required( ErrorMessage = "Description field is required" )]
        public string Description { get; set; } = string.Empty;

        [EmailAddress( ErrorMessage = "Not valid Email address" )]
        [Required( ErrorMessage = "EmailAddress is required" )]
        public string? EmailAddress { get; set; }

        [Required( ErrorMessage = "Phone number is required" )]
        [DataType( DataType.PhoneNumber , ErrorMessage = "Not valid phone number" )]
        [StringLength( 11 , ErrorMessage = "Length of phone number must be 11 number" )]
        public string phoneNum { get; set; } = string.Empty;
        [Required( ErrorMessage = "Please login as restaurant admin" )]
        public string ResAdminID { get; set; } = string.Empty;
      //  [Required( ErrorMessage = "Please upload a photo of your restaurant" )]
     
        public IFormFile? PosterFile { get; set; }
       
      // [Required( ErrorMessage = "Please upload a Banner photo for your restaurant" )]
       
        public IFormFile? BannearFile { get; set; }
        public bool IsAccept { get; set; }

    }
}
