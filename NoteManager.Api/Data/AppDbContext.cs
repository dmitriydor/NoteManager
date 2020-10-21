using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<File> Files { get; set; }
    }
}