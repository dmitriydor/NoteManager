﻿namespace NoteManager.Api.Contracts.Requests
{
    public class RefreshRequest
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}