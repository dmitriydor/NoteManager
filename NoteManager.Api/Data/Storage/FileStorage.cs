using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NoteManager.Api.Models;
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

        public string GetPath()
        {
            throw new System.NotImplementedException();
        }

        public Task<IFormFile> SaveFileAsync(IFormFile formFile, File fileModel)
        {
            throw new System.NotImplementedException();
        }

        public IFormFile GetFile(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateFile(IFormFile file, string fileName)
        {
            throw new System.NotImplementedException();
        }
    }
}