namespace Titanium2.Application.DTOs
{
    public class FavoriteDTO
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public Guid FavoriteGuid { get; set; } = Guid.NewGuid();
        public DateTime AddedAt { get; set; }
    }
}
