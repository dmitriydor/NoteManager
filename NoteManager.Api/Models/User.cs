using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace NoteManager.Api.Models
{
    public class User : IdentityUser
    {
        public DateTime CreatedDate { get; set; }

        [Required] public string FirstName { get; set; }

        [Required] public string LastName { get; set; }

        public string Biography { get; set; }
    }
}