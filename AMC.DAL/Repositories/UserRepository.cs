using AMC.DAL.Interfaces;
using AMC.CORE.Models;
using System.Data;
using Dapper;
using System.Linq;
using System;
using System.Collections.Generic;

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

        public TableResult<User> GetTable()
        {
            int count = conn.Execute("SELECT COUNT(*) FROM Users");
            IEnumerable<User> users = conn.Query<User>("SELECT * FROM Users");
            return new TableResult<User>(users, count);
        }

        public User Read(string username)
        {
            return conn.Query<User>("SELECT * FROM Users WHERE Username = @Username", new { Username = username }).SingleOrDefault();
        }

        public int Update(User user)
        {
            return conn.Execute("UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, IsRegistered = @IsRegistered, Role = @Role WHERE Id = @Id", user);
        }
    }
}
