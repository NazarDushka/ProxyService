using Proxy.Models;

namespace Proxy.Interfaces
{
    public interface IUserService
    {
         Task<User> GetUserById(int id);
    }
}
