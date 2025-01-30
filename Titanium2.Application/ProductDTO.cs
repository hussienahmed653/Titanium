using Microsoft.AspNetCore.Http;

namespace Titanium2.Application
{
    public class ProductDTO
    {
        public int? ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public IFormFile? ImagePath { get; set; }
        public int CategoryId { get; set; }
    }
}
