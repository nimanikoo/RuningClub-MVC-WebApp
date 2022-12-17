using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RuningClub_WebApp.Data;
using RuningClub_WebApp.Dtos;
using RuningClub_WebApp.Models;

namespace RuningClub_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDataContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, AppDataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View(loginDto);
            }
            var existUser = await _userManager.FindByEmailAsync(loginDto.EmailAddress);
            if (existUser != null)
            {
                //User founded and checking password
                var checkPassword = await _userManager.CheckPasswordAsync(existUser, loginDto.Password);
                if (checkPassword)
                {
                    //correct password
                    var result = await _signInManager.PasswordSignInAsync(existUser, loginDto.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }
                }
                // Wrong password
                TempData["Error"] = "Wrong Password";
                return View(loginDto);
            }
            //user not found
            TempData["Error"] = "Wrong credentials... try again";
            return View(loginDto);
        }

        public IActionResult Register()
        {
            return View();
        }



    }
}
