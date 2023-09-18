using ECommerce.DAL.Models.IdentityModels;

namespace ECommerce.DAL.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int itemID { get; set; }
        public MenuItem item { get; set; }
        public string UserID { get; set; }
        public ApplicationUser users { get; set; }
        public string Quantity { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
