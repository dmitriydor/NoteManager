using System;

namespace NoteManager.Api.Contracts.Requests
{
    public class NoteRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
    }
}