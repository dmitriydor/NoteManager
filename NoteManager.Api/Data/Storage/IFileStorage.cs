using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data.Storage
{
    public interface IFileStorage
    {
        public Task SaveFileAsync(IFormFile formFile, File fileModel);
        // имя файла в формате "{UserId}{FileId}"
        public IFormFile GetFile(string fileName);
        public bool UpdateFile(IFormFile file, string fileName);
    }
}