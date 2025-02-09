using Microsoft.EntityFrameworkCore;
using Titanium2.Application.DTOs;
using Titanium2.Application.Interfaces.CategoryInterfaces;
using Titanium2.Application.Interfaces.ImageInterface;
using Titanium2.Application.Interfaces.ProductInterfaces;
using Titanium2.Domain.File;

namespace Titanium2.Application.Services
{
    public class ImageServices
    {
        IImageInterface _imageServices;
        IproductInterface _productServices;
        ICategoryInterface _categoryServices;

        public ImageServices(IImageInterface imageServices, IproductInterface productServices, ICategoryInterface categoryServices)
        {
            _imageServices = imageServices;
            _productServices = productServices;
            _categoryServices = categoryServices;
        }
        private readonly string _imageFolderPath = "wwwroot/uploads/category";
        public async Task<string> AddFile(FileDTO fileDTO)
        {
            //get product by guid
            var productdata = await _productServices.GetProductByGuid(fileDTO.FolderGuid);
            var categoryname = await _categoryServices.GetCategoryByGuid(productdata.Category.CategoryGuid);
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
            var lastid = await _imageServices.LastId();
            var file = new FileModel
            {
                FileId = lastid + 1,
                FilePath = path,
                FolderGuid = fileDTO.FolderGuid,
                Extention = extention,
                Size = filesizeinMB
            };
            return await _imageServices.UplodeImage(file, path);
        }

        public async Task<bool> RemoveFile(Guid guid)
        {
            return await _imageServices.DeleteImage(guid);
        }
    }
}
