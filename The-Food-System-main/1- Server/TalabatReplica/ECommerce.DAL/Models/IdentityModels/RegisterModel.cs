using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models.IdentityModels
{
    public class RegisterModel
    {
        [Required, MaxLength(15)]
        public string FirstName { get; set; }

        [Required, MaxLength(15)]
        public string LastName { get; set; }

        [Required, MaxLength(50)]
        public string Username { get; set; }

        [Required, StringLength(128)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, StringLength(256)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool AdminCheck { get; set; } 

    }
}
