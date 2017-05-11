using AMC.DAL.Interfaces;
using AMC.CORE.Models;
using System.Data;
using Dapper;
using System.Linq;

namespace AMC.DAL.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        IDbConnection conn;
        public LocationRepository(IDbConnection connection)
        {
            conn = connection;
        }

        public int Create(Location location)
        {
            return conn.Execute("INSERT INTO Locations (Name, State, City, Zip, Adress1, Address2, Phone) OUTPUT INSERTED.Id VALUES (@Name, @State, @City, @Zip, @Adress1, @Address2, @Phone)", location);
        }

        public Location Read(int id)
        {
            return conn.Query<Location>("SELECT * FROM Locations WHERE Id = @Id", new { Id = id }).SingleOrDefault();
        }
    }
}
