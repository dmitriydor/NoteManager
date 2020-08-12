using System.ComponentModel.DataAnnotations;

namespace NoteManager.Api.Contracts.Requests
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}