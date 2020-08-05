using System;
using Microsoft.AspNetCore.Identity;

namespace NoteManager.Api.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Biography { get; set; }
    }
}