﻿using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _dbContext;
        
        public FileRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<File> GetByIdAsync(Guid fileId)
        {
            return await _dbContext.Files.FirstOrDefaultAsync(x => x.Id == fileId);
        }

        public async Task<File> GetByUserId(Guid userId)
        {
            return await _dbContext.Files.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<File> UpdateAsync(File file, bool committed)
        {
            var result = _dbContext.Files.Update(file).Entity;
            if (committed)
            {
                await CommitChanges();
            }

            return result;
        }

        public async Task<bool> DeleteAsync(File file, bool committed)
        {
            var result = _dbContext.Files.Remove(file).State == EntityState.Deleted;
            if (committed)
            {
                await CommitChanges();
            }

            return result;
        }

        public async Task<File> InsertAsync(File file, bool committed)
        {
            var result = (await _dbContext.Files.AddAsync(file)).Entity;
            if (committed)
            {
                await CommitChanges();
            }

            return result;
        }

        public async Task CommitChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}