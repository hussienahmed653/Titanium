using Microsoft.AspNetCore.Http;

namespace Titanium2.Application
{
    public class FileDTO
    {
        public Guid? FileGuid { get; set; } = Guid.NewGuid();
        public Guid FolderGuid { get; set; }
        public IFormFile FilePath { get; set; }
        public string Extention { get; set; } = string.Empty;
        public double? Size { get; set; }
    }
}
