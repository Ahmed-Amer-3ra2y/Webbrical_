using ECommerce.DAL.Models.IdentityModels;

namespace ECommerce.DAL.Models
{
    public class Order
    {
        public int orderID { get; set; }
        public int itemID { get; set; }
        public MenuItem item { get; set; }
        public string UserID { get; set; } = string.Empty;
        public ApplicationUser users { get; set; }
        public int totalPrice { get; set; }
    }
}
