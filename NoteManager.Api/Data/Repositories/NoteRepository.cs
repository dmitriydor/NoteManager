using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<Note> GetNoteById(Guid id)
        {
            return await _context.Notes.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Note>> GetNotesByUserId(string userId)
        {
            return await _context.Notes.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Note> UpsertNote(Note note)
        {
            var storedNote = await _context.Notes.SingleOrDefaultAsync(x => x.Id == note.Id);
            var result = storedNote == null ? (await _context.Notes.AddAsync(note)).Entity : _context.Notes.Update(note).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task DeleteNotes(params Note[] notes)
        {
            _context.Notes.RemoveRange(notes);
            await _context.SaveChangesAsync();
        }
    }
}