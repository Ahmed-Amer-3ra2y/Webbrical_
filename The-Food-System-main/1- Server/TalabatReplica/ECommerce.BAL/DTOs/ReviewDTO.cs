namespace ECommerce.BAL.DTOs
{
    public class ReviewDTO
    {
        public int ID { get; set; }
        public string CustomerID { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int ResID { get; set; }
    }
}
