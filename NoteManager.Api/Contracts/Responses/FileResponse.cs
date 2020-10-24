using System;

namespace NoteManager.Api.Contracts.Responses
{
    public class FileResponse
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
    }
}