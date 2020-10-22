using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NoteManager.Api.Data.Repositories;
using NoteManager.Api.Data.Storage;
using File = NoteManager.Api.Models.File;

namespace NoteManager.Api.Services
{
    public class FileService : IFileService
    {
        private readonly IFileStorage _fileStorage;
        private readonly IFileRepository _fileRepository;
        private readonly ILogger<FileService> _logger;
        
        public FileService(IFileStorage fileStorage, IFileRepository fileRepository, ILogger<FileService> logger)
        {
            _fileStorage = fileStorage;
            _fileRepository = fileRepository;
            _logger = logger;
        }
        public async Task<File> SaveFileAsync(IFormFile file, string userId)
        {
            //todo: валидация файла
            _logger.LogInformation("Uploading File by {UserId}", userId);
            File result;
            var fileModel = CreateFile(file, userId);
            
            try
            {
                result = await _fileRepository.InsertAsync(fileModel, false);
                await _fileStorage.SaveFileAsync(file, result);
                await _fileRepository.CommitChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Error while writing file", e);
                throw;
            }

            return result;
        }

        public Task<bool> UpdateFileAsync(IFormFile file, Guid fileId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteFileAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IFormFile> GetFileAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        private string GenerateFileName(string fileId, string userId)
        {
            return $"{fileId}-{userId}";
        }

        private File CreateFile(IFormFile file, string userId)
        {
            var id = Guid.NewGuid();
            var fileName = GenerateFileName(id.ToString(), userId);

            return new File
            {
                Id = id,
                CreationDate = DateTime.UtcNow.Date,
                Name = fileName,
                Size = file.Length,
                Format = Path.GetExtension(file.FileName),
                ContentType = file.ContentType,
                UserId = userId,
            };
        }
    }
}