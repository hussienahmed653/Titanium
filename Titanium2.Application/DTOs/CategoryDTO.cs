namespace Titanium2.Application.DTOs
{
    public class CategoryDTO
    {
        public Guid CategoryGuid { get; set; } = Guid.NewGuid();
        public string Categoryname { get; set; } = string.Empty;
    }
}
