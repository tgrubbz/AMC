using AMC.CORE.Models;

namespace AMC.DAL.Interfaces
{
    public interface ILocationRepository
    {
        int Create(Location location);
        Location Read(int id);
    }
}
