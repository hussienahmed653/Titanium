namespace Titanium2.Domain.File
{
    public class FileModel
    {
        public int FileId { get; set; }
        public Guid FileGuid { get; set; } = Guid.NewGuid();
        public Guid? FolderGuid { get; set; } // productguid
        public string FilePath { get; set; }
        public string Extention { get; set; }
        public double Size { get; set; }
    }
}
