using AMC.BLL.Models;
using AMC.CORE.Enumerations;
using AMC.CORE.Models;

namespace AMC.BLL.Interfaces
{
    public interface IUserManager
    {
        LoginResult Login(string username, string password);
        RegisterResult Register(string username, string password);

        int Create(User user);
        int Setup(User user);
        User Read(string username);
        int Delete(int id);
        int Update(User user);
        DataTableResult<User> GetUsersTable(DataTableRequest request);
    }
}
