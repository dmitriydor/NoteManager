using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using NoteManager.Domain.Models;

namespace NoteManager.Domain.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetTokenAsync(string token);
        Task<RefreshToken> UpdateTokenAsync(RefreshToken token);
        Task<RefreshToken> CreateTokenAsync(SecurityToken token, User user);
    }
}