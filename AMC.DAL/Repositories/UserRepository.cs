using AMC.DAL.Interfaces;
using AMC.CORE.Models;
using System.Data;
using Dapper;
using System.Linq;
using System.Collections.Generic;
using System;

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
            return conn.Execute("INSERT INTO Users (Username, PasswordHash, IsRegistered, IsSetup, Role) OUTPUT INSERTED.Id VALUES (@Username, @PasswordHash, @IsRegistered, @IsSetup, @Role)", user);
        }

        public int Delete(int id)
        {
            return conn.Execute("DELETE FROM Users WHERE Id = @Id", new { Id = id });
        }

        public DataTableResult<User> GetTable(DataTableRequest request)
        {
            if(request.Search != null)
            {
                foreach (var column in request.Columns)
                {
                    if (column.Searchable)
                    {

                    }
                }
            }

            int count = conn.Query<int>("SELECT COUNT(*) FROM Users").SingleOrDefault();
            IEnumerable<User> users = conn.Query<User>("SELECT * FROM Users");
            return new DataTableResult<User>(users, count);
        }

        public User Read(string username)
        {
            return conn.Query<User>("SELECT * FROM Users WHERE Username = @Username", new { Username = username }).SingleOrDefault();
        }

        public User Read(int id)
        {
            return conn.Query<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id }).SingleOrDefault();
        }

        public int Update(User user)
        {
            return conn.Execute("UPDATE Users SET Username = @Username, PasswordHash = @PasswordHash, IsRegistered = @IsRegistered, IsSetup = @IsSetup, Role = @Role WHERE Id = @Id", user);
        }
    }
}
