using System.ComponentModel.DataAnnotations;

namespace NoteManager.Api.Contracts.Requests
{
    public class RegistrationRequest
    {
        [EmailAddress] public string Email { get; set; }

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}