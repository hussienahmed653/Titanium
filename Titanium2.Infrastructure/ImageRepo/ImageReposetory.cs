using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;
using Titanium2.Application;
using Titanium2.Application.Interfaces.IImageInterface;
using Titanium2.Domain.File;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.ImageRepo
{
    public class ImageReposetory : IImageService
    {
        ApplicationDbContext _context;

        public ImageReposetory(ApplicationDbContext context)
        {
            _context = context;
        }
        private readonly string _imageFolderPath = "wwwroot/uploads/category";
        public async Task<string> UplodeImage(FileDTO fileDTO)
        {
            //get product by guid
            var productdata = await _context.Product.SingleOrDefaultAsync(p => p.ProductGuid == fileDTO.FolderGuid);
            var categoryname = await _context.Category.FindAsync(productdata.CategoryId);
            //check if product null or not
            if (productdata is null)
                return $"No Data found!";
            //check if file is null or not
            if (fileDTO.FilePath is null || fileDTO.FilePath.Length == 0)
                throw new Exception("Invalid Image file!");

            //check if i have the categoryfolder or not
            var categoryFolderPath = Path.Combine(_imageFolderPath, categoryname.CategoryName);
            Directory.CreateDirectory(categoryFolderPath);

            //check if i have the path for this product or not
            var productfolderpath = Path.Combine(categoryFolderPath, productdata.ProductName);
            Directory.CreateDirectory(productfolderpath);

            //get extention, create new filename
            var extention = Path.GetExtension(fileDTO.FilePath.FileName);
            var fileName = $"{Guid.NewGuid()}{extention}";
            var filePath = Path.Combine(productfolderpath, fileName);

            //get size of the file
            var filesizeinbytes = fileDTO.FilePath.Length;
            var filesizeinMB = filesizeinbytes / (1024.0 * 1024.0);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await fileDTO.FilePath.CopyToAsync(stream);
            }

            var path = $"{productfolderpath}/{fileName}";
            var lastid = await _context.Files.AnyAsync() ? await _context.Files.MaxAsync(f => f.FileId) : 0;
            var file = new FileModel
            {
                FileId = lastid+1,
                FilePath = path,
                Extention = extention,
                Size = filesizeinMB
            };
            await _context.Files.AddAsync(file);
            var affected = await _context.SaveChangesAsync();
            if(affected > 0)
                return path;
            return null;
        }
    }
}
