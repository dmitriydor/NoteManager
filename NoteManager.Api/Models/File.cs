using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteManager.Api.Models
{
    public class File
    {
        public Guid Id { set; get; }
        public string Name { get; set; }
        public string Format { get; set; }
        public long Size { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
    }
}