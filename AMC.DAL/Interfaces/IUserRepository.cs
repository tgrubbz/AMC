using AMC.CORE.Models;

namespace AMC.DAL.Interfaces
{
    public interface IUserRepository
    {
        int Create(User user);
        TableResult<User> GetTable();
        User Read(string username);
        int Update(User user);
        //int Delete(int id);
    }
}
