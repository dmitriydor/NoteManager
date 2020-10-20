using Microsoft.AspNetCore.Http;

namespace NoteManager.Api.Data.Storage
{
    public interface IFileStorage
    {
        public string GetPath();
        public bool SaveFile(IFormFile file);
        // имя файла в формате "{UserId}{FileId}"
        public IFormFile GetFile(string fileName);
        public bool UpdateFile(IFormFile file, string fileName);
    }
}