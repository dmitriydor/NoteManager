﻿using System.Collections.Generic;

namespace NoteManager.Api.Contracts.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public IEnumerable<string> ErrorMessages { get; set; }
    }
}