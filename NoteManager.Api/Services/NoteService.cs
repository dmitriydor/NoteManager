using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NoteManager.Api.Data.Repositories;
using NoteManager.Api.Models;

namespace NoteManager.Api.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly ILogger<NoteService> _logger;

        public NoteService(INoteRepository noteRepository, ILogger<NoteService> logger)
        {
            _noteRepository = noteRepository;
            _logger = logger;
        }

        public async Task<Note> GetNoteByIdAsync(string id)
        {
            _logger.LogInformation("Get note {Id}", id);
            return await _noteRepository.GetNoteByIdAsync(Guid.Parse(id));
        }

        public async Task<List<Note>> GetNotesByUserIdAsync(string userId)
        {
            _logger.LogInformation("Get all notes for {UserId}", userId);
            return await _noteRepository.GetNotesByUserIdAsync(userId);
        }

        public async Task<Note> UpsertNoteAsync(Note note)
        {
            _logger.LogInformation("Upsert note {Note}", note);
            return await _noteRepository.UpsertNoteAsync(note);
        }

        public async Task DeleteNotesAsync(params Note[] notes)
        {
            _logger.LogInformation("Delete notes {Notes}", notes);
            await _noteRepository.DeleteNotesAsync(notes);
        }
    }
}