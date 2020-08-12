using System.Collections.Generic;

namespace NoteManager.Api.Models
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
    }
}