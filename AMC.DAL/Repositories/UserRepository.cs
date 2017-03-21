using AMC.DAL.Interfaces;
using AMC.CORE.Models;
using System.Data;
using Dapper;
using System.Linq;

namespace AMC.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        IDbConnection conn;

        public UserRepository(IDbConnection connection)
        {
            conn = connection;
        }

        public int Create(User user)
        {
            return conn.Execute("INSERT INTO Users (Username) OUTPUT INSERTED.Id VALUES (@Username)", user);
        }

        public User Read(string username)
        {
            return conn.Query<User>("SELECT * FROM Users WHERE Username = @Username", new { Username = username }).SingleOrDefault();
        }

        public int Update(User user)
        {
            return conn.Execute("UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, IsRegistered = @IsRegistered WHERE Id = @Id", user);
        }
    }
}
