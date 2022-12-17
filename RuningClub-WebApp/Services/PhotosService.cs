using Microsoft.Extensions.Options;
using RuningClub_WebApp.Data.Interfaces;
using RuningClub_WebApp.Models;

namespace RuningClub_WebApp.Services
{
    public class PhotosService : IPhotosService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PhotosService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment= webHostEnvironment;
        }
        public async Task<string> AddPhotosAsync(IFormFile file)
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            var imageAddres= fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            string path = Path.Combine(webRootPath + "/Image/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return imageAddres;
        }

        public void DeletePhotosAsync(string imageAddress)
        {
            throw new NotImplementedException();
        }
    }
}
