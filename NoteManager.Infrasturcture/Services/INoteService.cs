using NoteManager.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteManager.Infrasturcture.Services
{
    public interface INoteService
    {
        Task<Note> GetNoteByIdAsync(string id);
        Task<List<Note>> GetNotesByUserIdAsync(string userId);
        Task<Note> UpsertNoteAsync(Note note);
        Task DeleteNotesAsync(params Note[] notes);
    }
}