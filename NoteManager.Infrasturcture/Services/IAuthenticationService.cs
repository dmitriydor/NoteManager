using Microsoft.AspNetCore.Http;
using NoteManager.Domain.Models;
using System.Threading.Tasks;

namespace NoteManager.Infrasturcture.Services
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