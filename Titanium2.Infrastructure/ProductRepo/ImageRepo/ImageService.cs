using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titanium2.Application.Interfaces.IImageInterface;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.ProductRepo.ImageRepo
{
    public class ImageService : IImageService
    {
        ApplicationDbContext _context;

        public ImageService(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly string _imageFolderPath = "wwwroot/uploads/products";
        public async Task<string> UplodeImage(IFormFile image, int? sectionid)
        {
            var categorydata = await _context.Category.SingleOrDefaultAsync(c => c.CategoryId == sectionid);
            if (categorydata is null)
                return $"No Data found with this Id: {sectionid}";
            if (image is null || image.Length == 0)
                throw new Exception("Invalid Image file!");

            var sectionFolderPath = Path.Combine(_imageFolderPath, categorydata.CategoryName);
            Directory.CreateDirectory(sectionFolderPath);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(sectionFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return $"uploads/products/{sectionFolderPath}/{fileName}"; 
        }
    }
}
