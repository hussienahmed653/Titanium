using Microsoft.AspNetCore.Http;

namespace Titanium2.Application.DTOs
{
    public class FileDTO
    {
        public Guid? FileGuid { get; set; }
        public Guid FolderGuid { get; set; }
        public IFormFile FilePath { get; set; }
        public string Extention { get; set; } = string.Empty;
        public double? Size { get; set; }
    }
}
