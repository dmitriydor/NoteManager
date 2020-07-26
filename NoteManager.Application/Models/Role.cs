using Microsoft.AspNetCore.Identity;

namespace NoteManager.Application.Models
{
    public sealed class Role : IdentityRole<int>
    {
        public Role()
        {
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}