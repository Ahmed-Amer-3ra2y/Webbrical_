namespace ECommerce.DAL.Models
{
    public class MenuItem
    {
        public int ItemID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int price { get; set; }
        public byte[]?image { get; set; }
        public string Description { get; set; } = string.Empty;
        public string size { get; set; } = string.Empty;
        public int ResturantID { get; set; }
        public Resturant resturant { get; set; }
        public int CategoryID { get; set; }
        public Category category { get; set; }
        public List<Order> Orders { get; set; }
        public List<Cart> Carts { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsTopItem { get; set; }
        public bool Offer { get; set; }    

    }
}
