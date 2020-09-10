using System.Collections.Generic;

namespace NoteManager.Api.Contracts.Responses
{
    public class AuthResponse
    {
        public bool IsAuthenticated { get; set; }
        public string AccessToken { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}