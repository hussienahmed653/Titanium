namespace Titanium2.Application.DTOs
{
    public class CartItemDTO
    {
        public int? CartItemId { get; set; }
        public Guid? CartItemGuid { get; set; } = Guid.NewGuid();
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
    }
}
