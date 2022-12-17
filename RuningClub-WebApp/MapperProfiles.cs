using AutoMapper;
using RuningClub_WebApp.Dtos;
using RuningClub_WebApp.Models;

namespace RuningClub_WebApp
{
    public class MapperProfiles:Profile
    {
        public MapperProfiles()
        {
            CreateMap<ClubCreateDto, Club>();
            CreateMap<Club,ClubCreateDto>();
        }

    }
}
