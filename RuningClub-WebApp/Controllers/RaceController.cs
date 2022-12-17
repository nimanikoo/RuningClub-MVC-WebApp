using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuningClub_WebApp.Data;
using RuningClub_WebApp.Data.Interfaces;
using RuningClub_WebApp.Dtos;
using RuningClub_WebApp.Models;
using RuningClub_WebApp.Services;

namespace RuningClub_WebApp.Controllers
{
    public class RaceController : Controller
    {
        private readonly AppDataContext _context;
        private readonly IRaceService _raceService;
        private readonly IPhotosService _photosService;
        public RaceController(AppDataContext context, IRaceService raceService, IPhotosService photosService)
        {
            _context = context;
            _raceService = raceService;
            _photosService = photosService;
        }
        public async Task<IActionResult> Index()
        {
            var raceList = await _raceService.GetAll();
            return View(raceList);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Race race = await _raceService.GetByIdAsync(id);
            return View(race);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RaceCreateDto raceDto)
        {
            if (ModelState.IsValid)
            {
                var savedImage = await _photosService.AddPhotosAsync(raceDto.Image);
                var race = new Race
                {
                    Title = raceDto.Title,
                    Description = raceDto.Description,
                    Image = savedImage,
                    RaceCategory = raceDto.RaceCategory,
                    Address = new Address
                    {
                        City = raceDto.Address.City,
                        Street = raceDto.Address.Street,
                        State = raceDto.Address.State,
                    }
                };
                _raceService.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Uploading was Failed...");
            }
            return View(raceDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var existRace = await _raceService.GetByIdAsync(id);
            if (existRace == null) return View();
            var clubDto = new EditRaceDto
            {
                Title = existRace.Title,
                RaceCategory = existRace.RaceCategory,
                Description = existRace.Description,
                AddressId = existRace.AddressId,
                Address = existRace.Address,
                URL = existRace.Image
            };
            return View(clubDto);
        }

         [HttpPost]
        public async Task<IActionResult> Edit(int id,EditRaceDto editRaceDto)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit",editRaceDto);
            }
            var existClub = await _raceService.GetByIdAsync(id);
            if(existClub != null)
            {
                try
                {
                     _photosService.DeletePhotosAsync(existClub.Image);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "Could not Delete photo");
                    return View(editRaceDto);
                }
                var photoResult = await _photosService.AddPhotosAsync(editRaceDto.Image);
                var race = new Race
                {
                    Id= id,
                    Title= editRaceDto.Title,
                    Image= photoResult,
                    Description= editRaceDto.Description,
                    AddressId= editRaceDto.AddressId,
                    Address= editRaceDto.Address,
                };
                _raceService.Update(race);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editRaceDto);
            }

        }


    }
}

