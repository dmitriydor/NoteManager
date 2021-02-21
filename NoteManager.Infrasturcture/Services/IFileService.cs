using Microsoft.AspNetCore.Http;
using NoteManager.Domain.Models;
using System.Threading.Tasks;

namespace NoteManager.Infrasturcture.Services
{
    public interface IFileService
    {
        Task<File> SaveFileAsync(IFormFile file, string userId);
        Task DeleteFileAsync(string userId);
        Task<File> GetFileAsync(string userId);
    }
}