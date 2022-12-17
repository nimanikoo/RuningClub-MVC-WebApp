using RuningClub_WebApp.Models;

namespace RuningClub_WebApp.Data.Interfaces
{
    public interface IRaceService
    {
        Task<IEnumerable<Race>> GetAll();
        Task<Race> GetByIdAsync(int id);
        Task<IEnumerable<Race>> GetRacesByCityAsync(string city);
        bool Add(Race race);
        bool Delete(Race race);
        bool Update(Race race);
        bool SaveAsync();
    }
}
