using Titanium2.Domain.Cart;
using Titanium2.Domain.Product;

namespace Titanium2.Domain.CartItem
{
    public class CartItemModel
    {
        public int CartItemId { get; set; }
        public Guid CartItemGuid { get; set; } = Guid.NewGuid();
        public int CartId { get; set; }
        public CartModel Cart { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalPrice => (Quantity * Product?.Price ?? 0);
    }
}
