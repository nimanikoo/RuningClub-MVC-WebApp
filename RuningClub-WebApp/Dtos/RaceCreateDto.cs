using RuningClub_WebApp.Models.Enums;
using RuningClub_WebApp.Models;

namespace RuningClub_WebApp.Dtos
{
    public class RaceCreateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
