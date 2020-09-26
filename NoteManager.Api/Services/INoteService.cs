using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteManager.Api.Models;

namespace NoteManager.Api.Services
{
    public interface INoteService
    {
        Task<Note> GetNoteByIdAsync(string id);
        Task<List<Note>> GetNotesByUserIdAsync(string userId);
        Task<Note> UpsertNoteAsync(Note note);
        Task DeleteNotesAsync(params Note[] notes);
    }
}