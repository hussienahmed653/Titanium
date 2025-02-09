using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Titanium2.Application.DTOs;
using Titanium2.Domain.File;

namespace Titanium2.Application.Interfaces.ImageInterface
{
    public interface IImageInterface
    {
        public Task<string> UplodeImage(FileModel fileDTO, string path);
        public Task<bool> DeleteImage(Guid fileguid);

        public Task<int> LastId();
    }
}
