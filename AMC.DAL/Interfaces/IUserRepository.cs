using AMC.CORE.Models;

namespace AMC.DAL.Interfaces
{
    public interface IUserRepository
    {
        int Create(User user);
        //IEnumerable<User> GetAll();
        User Read(string username);
        int Update(User user);
        //int Delete(int id);
    }
}
