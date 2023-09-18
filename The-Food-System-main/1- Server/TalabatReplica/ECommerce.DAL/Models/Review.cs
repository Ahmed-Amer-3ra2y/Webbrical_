using ECommerce.DAL.Models.IdentityModels;

namespace ECommerce.DAL.Models
{
    public class Review
    {
        public int ID { get; set; }
        public string CustomerID { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int ResID { get; set; }
        public ApplicationUser? Customer { get; set; }
        public Resturant? Restaurant { get; set; }

    }
}
