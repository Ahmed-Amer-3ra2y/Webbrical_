using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.DTOs
{
    public class ResetPasswordDto
    {
        [Required( ErrorMessage = "Not valid User" )]
        [EmailAddress( ErrorMessage = "Not Valid Email Address" )]
        public string UserID { get; set; } = string.Empty;


        [Required( ErrorMessage = "Password is required" )]
        [StringLength( 255 , ErrorMessage = "Must be between 5 and 255 characters" , MinimumLength = 5 )]
        [DataType( DataType.Password )]
        public string NewPassword { get; set; } = string.Empty;


        [Required( ErrorMessage = "Confirm Password is required" )]
        [StringLength( 255 , ErrorMessage = "Must be between 5 and 255 characters" , MinimumLength = 5 )]
        [Compare( "NewPassword" )]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required( ErrorMessage = "Token is required" )]
        public string Token { get; set; } = string.Empty;
    }
}
