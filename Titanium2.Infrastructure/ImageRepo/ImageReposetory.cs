using Microsoft.EntityFrameworkCore;
using Titanium2.Application.Interfaces.ImageInterface;
using Titanium2.Domain.File;
using Titanium2.Infrastructure.AppDbContext;

namespace Titanium2.Infrastructure.ImageRepo
{
    public class ImageReposetory : IImageInterface
    {
        ApplicationDbContext _context;

        public ImageReposetory(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<string> UplodeImage(FileModel file, string path)
        {
            await _context.Files.AddAsync(file);
            var affected = await _context.SaveChangesAsync();
            if(affected > 0)
                return path;
            return null;
        }

        public async Task<bool> DeleteImage(Guid fileguid)
        {
            var file = await _context.Files.FirstOrDefaultAsync(f => f.FileGuid == fileguid);

            if (file == null)
                throw new Exception("File not found!");

            var filepath = Path.Combine(file.FilePath);

            if(System.IO.File.Exists(filepath))
            {
                _context.Files.Remove(file);
                var affected = await _context.SaveChangesAsync();
                if(affected > 0)
                {
                    System.IO.File.Delete(filepath);
                    return true;
                }
            }
                return false;
        }

        public async Task<int> LastId()
        {
            return await _context.Files.AnyAsync() ? await _context.Files.MaxAsync(f => f.FileId) : 0;
        }
    }
}
