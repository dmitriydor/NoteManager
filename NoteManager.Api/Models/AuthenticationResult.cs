using System.Collections.Generic;

namespace NoteManager.Api.Models
{
    public class AuthenticationResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; } = new List<string>();
    }
}