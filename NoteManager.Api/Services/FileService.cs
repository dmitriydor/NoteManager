using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NoteManager.Api.Data.Repositories;
using NoteManager.Api.Data.Storage;
using NoteManager.Api.Models;

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
        public async Task<File> SaveFileAsync(IFormFile file, Guid userId)
        {
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

        private string GenerateFileName()
        {
            return null;
        }

        private File CreateFile(IFormFile file, Guid userId) => new File
        {
            CreationDate = DateTime.UtcNow.Date,
            Name = GenerateFileName(),
            Size = file.Length,
            Format = file.ContentType,
            UserId = userId,
        };
    }
}