using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NoteManager.Domain.Models;

namespace NoteManager.Domain.Storage
{
    public interface IFileStorage
    {
        public Task SaveFileAsync(IFormFile formFile, File fileModel);
        public Task DeleteFileAsync(File file);
    }
}