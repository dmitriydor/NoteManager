using System;
using System.Threading.Tasks;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data.Repositories
{
    public interface IFileRepository
    {
        Task<File> GetByIdAsync(Guid fileId);
        Task<File> GetByUserIdAsync(string userId);
        Task<File> UpdateAsync(File file, bool committed = true);
        Task<bool> DeleteAsync(File file, bool committed = true);
        Task<File> InsertAsync(File file, bool committed = true);
        Task CommitChanges();
    }
}