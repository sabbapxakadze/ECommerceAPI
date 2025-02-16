namespace DomainLibrary.Models
{
    public class Cart
    {
        public string Id { get; set; } // Unique Cart ID
        public string UserId { get; set; } // User who owns the cart
        public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>(); // Initialize to empty list
    }
}