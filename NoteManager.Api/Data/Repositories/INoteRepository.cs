using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetNoteById(Guid id);
        Task<List<Note>> GetNotesByUserId(string userId);
        Task<Note> UpsertNote(Note note);
        Task DeleteNotes(params Note[] notes);
    }
}