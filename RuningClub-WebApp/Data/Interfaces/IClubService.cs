using RuningClub_WebApp.Models;

namespace RuningClub_WebApp.Data.Interfaces
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int id);
        Task<IEnumerable<Club>> GetClubByCityAsync(string city);
        bool Add(Club club);
        bool Delete(Club club);
        bool Update(Club club);
        bool SaveAsync();
    }
}
