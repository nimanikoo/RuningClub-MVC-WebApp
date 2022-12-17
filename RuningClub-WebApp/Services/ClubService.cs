using Microsoft.EntityFrameworkCore;
using RuningClub_WebApp.Data;
using RuningClub_WebApp.Data.Interfaces;
using RuningClub_WebApp.Models;
using System.Diagnostics;

namespace RuningClub_WebApp.Services
{
    public class ClubService : IClubService
    {
        private readonly AppDataContext _context;
        public ClubService(AppDataContext context)
        {
            _context = context;
        }
        public bool Add(Club club)
        {
            _context.Add(club);
            return SaveAsync();
        }

        public bool Delete(Club club)
        {
            _context.Remove(club);
            return SaveAsync();
        }

        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();

        }

        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.Include(a => a.Address).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async  Task<IEnumerable<Club>> GetClubByCityAsync(string city)
        {
            return await _context.Clubs.Where( c => c.Address.City
            .Contains(city)).ToListAsync();
        }

        public  bool SaveAsync()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Club club)
        {
            _context.Update(club);
            return SaveAsync();
        }
    }
}
