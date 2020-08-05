using System.Collections.Generic;

namespace NoteManager.Api.Contracts.Responses
{
    public class RegistrationResponse
    {
        public string Token { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}