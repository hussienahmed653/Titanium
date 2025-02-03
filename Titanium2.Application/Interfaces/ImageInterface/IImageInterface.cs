using Titanium2.Domain.File;

namespace Titanium2.Application.Interfaces.ImageInterface
{
    public interface IImageInterface
    {
        public Task<string> UplodeImage(FileDTO fileDTO);
        public Task<bool> DeleteImage(Guid fileguid);
    }
}
