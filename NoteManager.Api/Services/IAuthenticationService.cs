using System.Threading.Tasks;
using NoteManager.Api.Models;

namespace NoteManager.Api.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResult> RegistrationAsync(string email, string password, string firstName, string lastName);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}