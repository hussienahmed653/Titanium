using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Titanium2.Application.Interfaces.IImageInterface;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.ImageRepo
{
    public class ImageService : IImageService
    {
        ApplicationDbContext _context;

        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly string _imageFolderPath = "wwwroot/uploads/products";
        public async Task<string> UplodeImage(IFormFile image, int? categoryid)
        {
            var categorydata = await _context.Category.SingleOrDefaultAsync(c => c.CategoryId == categoryid);
            if (categorydata is null)
                return $"No Data found with this Id: {categoryid}";
            if (image is null || image.Length == 0)
                throw new Exception("Invalid Image file!");

            var categoryFolderPath = Path.Combine(_imageFolderPath, categorydata.CategoryName);
            Directory.CreateDirectory(categoryFolderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(categoryFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return $"uploads/products/{categoryFolderPath}/{fileName}";
        }
    }
}
