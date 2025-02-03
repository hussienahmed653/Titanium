using Titanium2.Domain.File;

namespace Titanium2.Application.Interfaces.IImageInterface
{
    public interface IImageService
    {
        public Task<string> UplodeImage(FileDTO fileDTO);
        public Task<bool> DeleteImage(Guid fileguid);
    }
}
