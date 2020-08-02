using System.Threading.Tasks;
using DatingApp.Models;

namespace DatingApp.Services
{
    public interface IAuthRepository
    {
         Task<User> Register(User newuser , string password);
         Task<User> Login(string username, string password);
         Task<bool> UserExists(string username);
    }
}