using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<RefreshToken> GetTokenAsync(string token)
        {
            var refreshToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Token == token);
            return refreshToken;
        }

        public async Task<RefreshToken> UpdateTokenAsync(RefreshToken token)
        {
            var result = _context.RefreshTokens.Update(token);
            await _context.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<RefreshToken> CreateTokenAsync(SecurityToken token, User user)
        {
            var refreshToken = new RefreshToken
            {
                Jti = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(1)
            };
            var result = await _context.RefreshTokens.AddAsync(refreshToken);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}