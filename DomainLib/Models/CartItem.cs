
namespace DomainLibrary.Models
{
    public class CartItem
    {
        public string Id { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice => Price * Quantity;

        public Cart Cart { get; set; } // Navigation Property to Cart
        public Product Product { get; set; } // Navigation Property to Product
    }
}