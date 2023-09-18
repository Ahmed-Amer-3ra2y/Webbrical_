using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Models.IdentityModels
{
    public class RevokeModel
    {
        //will be allow null because use may nor send token and in this case i will recieve it from cookie
        public string? Token { get; set; }
    }
}
