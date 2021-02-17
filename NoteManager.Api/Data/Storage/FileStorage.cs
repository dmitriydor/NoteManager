using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using File = NoteManager.Api.Models.File;
using FileOptions = NoteManager.Api.Options.FileOptions;

namespace NoteManager.Api.Data.Storage
{
    public class FileStorage : IFileStorage
    {
        private readonly FileOptions _fileOptions;

        public FileStorage(IOptions<FileOptions> fileStorageOptions)
        {
            _fileOptions = fileStorageOptions.Value;

            if (!Directory.Exists(_fileOptions.DirectoryPath)) Directory.CreateDirectory(_fileOptions.DirectoryPath);
        }

        public async Task SaveFileAsync(IFormFile formFile, File fileModel)
        {
            var path = Path.Combine(_fileOptions.DirectoryPath, $"{fileModel.Name}{fileModel.Format}");
            await using var stream = new FileStream(path, FileMode.Create);
            await formFile.CopyToAsync(stream);
        }

        public Task DeleteFileAsync(File file)
        {
            return Task.Run(() =>
            {
                System.IO.File.Delete(Path.Combine(_fileOptions.DirectoryPath, $"{file.Name}{file.Format}"));
            });
        }
    }
}