using AMC.BLL.Models;
using AMC.CORE.Models;

namespace AMC.BLL.Interfaces
{
    public interface IUserManager
    {
        LoginResult Login(string username, string password);
        RegisterResult Register(string username, string password);

        int Create(string username);
        User Read(string username);
        int Update(User user);
    }
}
