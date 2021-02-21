using NoteManager.Domain.Models;
using System.Threading.Tasks;

namespace NoteManager.Infrasturcture.Services
{
    public interface IUserService
    {
        Task<User> EditUserAsync(User user);
    }
}