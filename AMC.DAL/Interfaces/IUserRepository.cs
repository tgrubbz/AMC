using AMC.CORE.Models;

namespace AMC.DAL.Interfaces
{
    public interface IUserRepository
    {
        int Create(User user);
        DataTableResult<User> GetTable(DataTableRequest request);
        User Read(string username);
        User Read(int id);
        int Update(User user);
        int Delete(int id);
    }
}
