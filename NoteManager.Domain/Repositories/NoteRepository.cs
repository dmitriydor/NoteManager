﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteManager.Domain.Data;
using NoteManager.Domain.Models;

namespace NoteManager.Domain.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Note> GetNoteByIdAsync(Guid id)
        {
            return await _context.Notes.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Note>> GetNotesByUserIdAsync(string userId)
        {
            return await _context.Notes.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Note> UpsertNoteAsync(Note note)
        {
            var storedNote = await _context.Notes.SingleOrDefaultAsync(x => x.Id == note.Id);
            var result = storedNote == null
                ? (await _context.Notes.AddAsync(note)).Entity
                : _context.Notes.Update(note).Entity;
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task DeleteNotesAsync(params Note[] notes)
        {
            _context.Notes.RemoveRange(notes);
            await _context.SaveChangesAsync();
        }
    }
}