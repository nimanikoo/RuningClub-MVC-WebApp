using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuningClub_WebApp.Data;
using RuningClub_WebApp.Data.Interfaces;
using RuningClub_WebApp.Dtos;
using RuningClub_WebApp.Models;

namespace RuningClub_WebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IPhotosService _photosService;
        private readonly IClubService _clubService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ClubController(IPhotosService photosService, IClubService clubService, IWebHostEnvironment webHostEnvironment)
        {
            _photosService = photosService;
            _clubService = clubService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var clubList = await _clubService.GetAll();
            return View(clubList);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Club club = await _clubService.GetByIdAsync(id);
            return View(club);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClubCreateDto clubDto)
        {
            if (ModelState.IsValid)
            {
                var savedImage = await _photosService.AddPhotosAsync(clubDto.Image);
                var club = new Club
                {
                    Title = clubDto.Title,
                    Description = clubDto.Description,
                    Image = savedImage,
                    ClubCategory = clubDto.ClubCategory,
                    Address = new Address
                    {
                        City = clubDto.Address.City,
                        Street = clubDto.Address.Street,
                        State = clubDto.Address.State,
                    }
                };
                _clubService.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Uploading was Failed...");
            }
            return View(clubDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var existClub = await _clubService.GetByIdAsync(id);
            if (existClub == null) return View();
            var clubDto = new EditClubDto
            {
                Title = existClub.Title,
                ClubCategory = existClub.ClubCategory,
                Description = existClub.Description,
                AddressId = existClub.AddressId,
                Address = existClub.Address,
                URL = existClub.Image
            };
            return View(clubDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditClubDto editClubDto)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit",editClubDto);
            }
            var existClub = await _clubService.GetByIdAsync(id);
            if(existClub != null)
            {
                try
                {
                     _photosService.DeletePhotosAsync(existClub.Image);
                }
                catch (Exception ex)
                {

                    ModelState.AddModelError("", "Could not Delete photo");
                    return View(editClubDto);
                }
                var photoResult = await _photosService.AddPhotosAsync(editClubDto.Image);
                var club = new Club
                {
                    Id= id,
                    Title= editClubDto.Title,
                    Image= photoResult,
                    Description= editClubDto.Description,
                    AddressId= editClubDto.AddressId,
                    Address= editClubDto.Address,
                };
                _clubService.Update(club);
                return RedirectToAction("Index");
            }
            else
            {
                return View(editClubDto);
            }

        }
    }
}
