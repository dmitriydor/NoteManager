using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using NoteManager.Api.Models;

namespace NoteManager.Api.Data.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetTokenAsync(string token);
        Task<RefreshToken> UpdateTokenAsync(RefreshToken token);
        Task<RefreshToken> CreateTokenAsync(SecurityToken token, User user);
    }
}