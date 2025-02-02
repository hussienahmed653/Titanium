namespace Titanium2.Application.Interfaces.IImageInterface
{
    public interface IImageService
    {
        public Task<string> UplodeImage(FileDTO fileDTO);
    }
}
