using ECommerce.DAL.Models.IdentityModels;

namespace ECommerce.DAL.Models
{
    public class Resturant
    {

        public int RestaurantID { get; set; }

        public string Location { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public byte[ ] Poster { get; set; }

        public string Description { get; set; } = string.Empty;

        public string? EmailAddress { get; set; }

        public string phoneNum { get; set; } = string.Empty;

        public virtual List<MenuItem> MenuItems { get; set; } = new List<MenuItem>( );
        public string ResAdminID { get; set; } = string.Empty;
        public ApplicationUser ApplicationResAdmin { get; set; }
        public bool IsAccept { get; set; }
        public List<Review>? Reviews { get; set; }
        public byte[ ] CoverBanner { get; set; }
    }
}
