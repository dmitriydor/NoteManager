using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NoteManager.Api.Models;

namespace NoteManager.Api.Services
{
    public interface IFileService
    {
        Task<File> SaveFileAsync(IFormFile file, string userId);
        Task DeleteFileAsync(string userId);
        Task<File> GetFileAsync(string userId);
    }
}