namespace Titanium2.Application.DTOs
{
    public class CartDTO
    {
        public int? CartId { get; set; }
        public Guid CartGuid { get; set; } = Guid.NewGuid();
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
