using Titanium2.Application.Interfaces.IImageInterface;

namespace Titanium2.Application.Services
{
    public class ImageServices
    {
        IImageService _imageServices;

        public ImageServices(IImageService imageServices)
        {
            _imageServices = imageServices;
        }
        public async Task<string> AddFile(FileDTO fileDTO)
        {
            return await _imageServices.UplodeImage(fileDTO);
        }
    }
}
