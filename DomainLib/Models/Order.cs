namespace DomainLibrary.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }


        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal FullAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}