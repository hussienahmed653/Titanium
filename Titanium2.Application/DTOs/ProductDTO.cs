using Microsoft.AspNetCore.Http;

namespace Titanium2.Application.DTOs
{
    public class ProductDTO
    {
        public Guid ProductGuid { get; set; } = Guid.NewGuid();
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
