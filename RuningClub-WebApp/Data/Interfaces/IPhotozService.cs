using Microsoft.AspNetCore.Mvc;

namespace RuningClub_WebApp.Data.Interfaces
{
    public interface IPhotosService
    {
        Task<string> AddPhotosAsync(IFormFile file);
        void DeletePhotosAsync(string imageAddress);
    }
}
