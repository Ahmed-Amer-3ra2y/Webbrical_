using ECommerce.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BAL.DTOs
{
    public class ResAdminConfirmDto
    {
        public string id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirm { get; set; }
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        public string UserName { get; set; }
    }

}
