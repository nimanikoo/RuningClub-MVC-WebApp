using RuningClub_WebApp.Models.Enums;
using RuningClub_WebApp.Models;

namespace RuningClub_WebApp.Dtos
{
    public class EditRaceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public string URL { get; set; }
        public RaceCategory RaceCategory { get; set; }
    }
}
