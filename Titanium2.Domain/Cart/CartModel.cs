using Titanium2.Domain.UsersRoles;

namespace Titanium2.Domain.Cart
{
    public class CartModel
    {
        public int CartId { get; set; }
        public Guid CartGuid { get; set; }
        public int UserId { get; set; }
        public UsersModel User { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
