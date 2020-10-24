using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NoteManager.Api.Models;

namespace NoteManager.Api.Services
{
    public interface IFileService
    {
        Task<File> SaveFileAsync(IFormFile file, string userId);
        Task<bool> DeleteFileAsync(Guid id);
        Task<IFormFile> GetFileAsync(Guid id);
    }
}