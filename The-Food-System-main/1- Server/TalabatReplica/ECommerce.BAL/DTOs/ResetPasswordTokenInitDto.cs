using System.ComponentModel.DataAnnotations;

namespace ECommerce.BAL.DTOs
{
    public class ResetPasswordTokenInitDto
    {
        [Required( ErrorMessage = "Email is required" )]
        [EmailAddress( ErrorMessage = "Email Not In A Valid Format" )]
        public string UserEmailAddress { get; set; } = string.Empty;

        //   public string? PasswordToken { get; set; } = string.Empty;
    }
}
