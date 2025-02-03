using Titanium2.Application.Interfaces.ImageInterface;
using Titanium2.Domain.File;

namespace Titanium2.Application.Services
{
    public class ImageServices
    {
        IImageInterface _imageServices;

        public ImageServices(IImageInterface imageServices)
        {
            _imageServices = imageServices;
        }
        public async Task<string> AddFile(FileDTO fileDTO)
        {
            return await _imageServices.UplodeImage(fileDTO);
        }

        public async Task<bool> RemoveFile(Guid guid)
        {
            return await _imageServices.DeleteImage(guid);
        }
    }
}
