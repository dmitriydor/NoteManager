using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetNoteByIdAsync(Guid id);
        Task<List<Note>> GetNotesByUserIdAsync(string userId);
        Task<Note> UpsertNoteAsync(Note note);
        Task DeleteNotesAsync(params Note[] notes);
    }
}