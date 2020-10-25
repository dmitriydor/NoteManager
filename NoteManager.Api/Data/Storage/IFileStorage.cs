using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data.Storage
{
    public interface IFileStorage
    {
        public Task SaveFileAsync(IFormFile formFile, File fileModel);
        public Task DeleteFileAsync(File file);
    }
}