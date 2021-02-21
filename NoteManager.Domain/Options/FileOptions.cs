using System.Collections.Generic;

namespace NoteManager.Domain.Options
{
    public class FileOptions
    {
        public string DirectoryPath { get; set; }
        public List<string> AllowedFormats { get; set; }
        public long MaxSize { get; set; }
    }
}