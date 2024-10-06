using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TitanicPassengers.Models;
using TitanicPassengers.Repositories;
using TitanicPassengers.ViewModels;
using Microsoft.AspNetCore.Authentication;

namespace TitanicPassengers.Controllers
{
	public class AccountController: Controller
	{
		private readonly UserRepository _userRepository;
		public AccountController(UserRepository userRepository)
		{
			_userRepository = userRepository;
		}

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userRepository.Login(model.Login, model.Password);

                    var claims = new List<Claim> {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim("Role", user.Role.ToString())};

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("GetAllParticipants", "Participants");
                }

                catch (InvalidDataException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (InvalidTimeZoneException)
                {
                    ModelState.AddModelError("", "Data access layer error");
                }
            }
            return View(model);
        }



        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(AccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newUser = new User(model.Login, model.Password, Models.Enums.Role.User);
                    var user = await _userRepository.Signup(newUser);

                    var claims = new List<Claim> {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Login),
                    new Claim("Role", user.Role.ToString())};

                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToAction("GetAllParticipants", "Participants");
                }

                catch (InvalidDataException e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data access layer error");
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GetAllParticipants", "Participants");
        }
    }
}

