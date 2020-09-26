using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoteManager.Api.Data.Repositories;
using NoteManager.Api.Models;

namespace NoteManager.Api.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public async Task<Note> GetNoteByIdAsync(string id)
        {
            
            return await _noteRepository.GetNoteByIdAsync(Guid.Parse(id));
        }

        public async Task<List<Note>> GetNotesByUserIdAsync(string userId)
        {
            return await _noteRepository.GetNotesByUserIdAsync(userId);
        }

        public async Task<Note> UpsertNoteAsync(Note note)
        {
            return await _noteRepository.UpsertNoteAsync(note);
        }

        public async Task DeleteNotesAsync(params Note[] notes)
        {
            await _noteRepository.DeleteNotesAsync(notes);
        }
    }
}