using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NoteManager.Api.Models;

namespace NoteManager.Api.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> RegistrationAsync(string email, string password, string firstName, string lastName);
        Task<AuthenticationResult> LoginAsync(string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string accessToken, string refreshToken);
        Task SetRefreshTokenInCookie(RefreshToken refreshToken, HttpContext context);
        Task SetAccessTokenInCookie(string accessToken, HttpContext context);
    }
}