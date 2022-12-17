using Microsoft.EntityFrameworkCore;
using RuningClub_WebApp.Data;
using RuningClub_WebApp.Data.Interfaces;
using RuningClub_WebApp.Models;

namespace RuningClub_WebApp.Services
{
    public class RaceService : IRaceService
    {
        private readonly AppDataContext _context;

        public RaceService(AppDataContext context)
        {
            _context = context;
        }

        public bool Add(Race race)
        {
            _context.Add(race);
            return SaveAsync();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return SaveAsync();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<Race> GetByIdAsync(int id)
        {
            return await _context.Races.
                Include(a => a.Address).AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Race>> GetRacesByCityAsync(string city)
        {
            return await _context.Races.Where(r => r.Address.City
                       .Contains(city)).ToListAsync();
        }

        public bool SaveAsync()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return SaveAsync();
        }
    }
}
