using Microsoft.Extensions.Options;
using NoteManager.Api.Options;

namespace NoteManager.Api.Data.Storage
{
    public class FileStorage : IFileStorage
    {
        private readonly FileStorageOptions _fileStorageOptions;
        public FileStorage(IOptions<FileStorageOptions> fileStorageOptions)
        {
            _fileStorageOptions = fileStorageOptions.Value;
        }
    }
}