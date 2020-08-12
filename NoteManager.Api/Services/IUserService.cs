using System.Threading.Tasks;
using NoteManager.Api.Models;


namespace NoteManager.Api.Services
{
    public interface IUserService
    {
        Task<User> EditUserAsync(User user);
    }
}