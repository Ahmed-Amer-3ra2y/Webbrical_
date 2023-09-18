using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BAL.DTOs
{
    public class ProfileUserDto
    {
       // public string? Id { get; set; }
        [Required, MaxLength(15)]
        public string FirstName { get; set; }

        [Required, MaxLength(15, ErrorMessage ="Max Length must be 15 character")]
        public string LastName { get; set; }

        //[Required, MaxLength(50, ErrorMessage = "Max Length must be 50 character")]
        //public string UserName { get; set; }

        //[Required, StringLength(128)]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        //[Required, StringLength(256)]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }
    }
}
