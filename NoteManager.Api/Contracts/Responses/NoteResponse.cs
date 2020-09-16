using System;

namespace NoteManager.Api.Contracts.Responses
{
    public class NoteResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string UserId { get; set; }
    }
}