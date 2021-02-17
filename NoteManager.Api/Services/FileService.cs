using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NoteManager.Api.Data.Repositories;
using NoteManager.Api.Data.Storage;
using File = NoteManager.Api.Models.File;
using FileOptions = NoteManager.Api.Options.FileOptions;

namespace NoteManager.Api.Services
{
    public class FileService : IFileService
    {
        private readonly FileOptions _fileOptions;
        private readonly IFileRepository _fileRepository;
        private readonly IFileStorage _fileStorage;
        private readonly ILogger<FileService> _logger;

        public FileService(IFileStorage fileStorage, IFileRepository fileRepository, ILogger<FileService> logger,
            IOptions<FileOptions> fileOptions)
        {
            _fileStorage = fileStorage;
            _fileRepository = fileRepository;
            _logger = logger;
            _fileOptions = fileOptions.Value;
        }

        //todo: переделать на контракты для UI
        public async Task<File> SaveFileAsync(IFormFile file, string userId)
        {
            _logger.LogInformation("Uploading File by {UserId}", userId);
            File result;
            try
            {
                if (file.Length > _fileOptions.MaxSize)
                    throw new Exception($"File size greater than {_fileOptions.MaxSize}", new FileLoadException());

                if (!_fileOptions.AllowedFormats.Contains(Path.GetExtension(file.FileName)))
                    throw new Exception($"Invalid image format. Supported {_fileOptions.AllowedFormats}");

                var entryFile = await _fileRepository.GetByUserIdAsync(userId);
                var fileModel = new File();
                if (entryFile == null)
                {
                    FillFile(ref fileModel, file, userId);
                    result = await _fileRepository.InsertAsync(fileModel, false);
                }
                else
                {
                    FillFile(ref entryFile, file, userId);
                    result = await _fileRepository.UpdateAsync(entryFile);
                }

                await _fileStorage.SaveFileAsync(file, result);
                await _fileRepository.CommitChanges();
            }
            catch (Exception e)
            {
                _logger.LogError($"Error while writing file. {e.Message}", e);
                throw;
            }

            _logger.LogInformation("File uploaded");
            return result;
        }

        //todo: переделать на контракты для UI
        public async Task DeleteFileAsync(string userId)
        {
            var file = await _fileRepository.GetByUserIdAsync(userId);
            if (file == null) throw new FileNotFoundException();
            try
            {
                await _fileRepository.DeleteAsync(file);
                await _fileStorage.DeleteFileAsync(file);
                await _fileRepository.CommitChanges();
            }
            catch (Exception e)
            {
                _logger.LogError("Deleting file error");
                throw;
            }
        }

        //todo: переделать на контракты для UI
        public async Task<File> GetFileAsync(string userId)
        {
            return await _fileRepository.GetByUserIdAsync(userId);
        }


        private void FillFile(ref File fileModel, IFormFile file, string userId)
        {
            fileModel.CreationDate = DateTime.UtcNow.Date;
            fileModel.Name ??= Path.GetRandomFileName();
            fileModel.Size = file.Length;
            fileModel.Format = Path.GetExtension(file.FileName);
            fileModel.ContentType = file.ContentType;
            fileModel.UserId = userId;
        }
    }
}