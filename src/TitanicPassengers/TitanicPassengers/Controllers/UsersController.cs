using System;
using Microsoft.AspNetCore.Mvc;
using TitanicPassengers.Models.Enums;
using TitanicPassengers.Repositories;
using TitanicPassengers.ViewModels;

namespace TitanicPassengers.Controllers
{
	public class UsersController: Controller
	{
        private readonly UserRepository _userRepository;
        public UsersController(UserRepository userRepository)
        {
            _userRepository = userRepository;

        }


        public async Task<IActionResult> GetAllUsers()
        {
            var model = new UsersViewModel();

            var user = HttpContext.User;
            var id = User.FindFirst("Id");
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated && id is not null)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    var uid = Int32.Parse(id.Value);
                    model.Users = await _userRepository.GetAllAsync(role);
                    model.Users.RemoveAll(u => u.Id == uid);
                    model.Role = role;
                    return View(model);
                }
                catch (InvalidCastException)
                {
                    ModelState.AddModelError("", "Data Access layer");
                    return View();
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");
        }


        public async Task<IActionResult> RemoveUser(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _userRepository.RemoveAsync(id, role);
                    return RedirectToAction("GetAllUsers", "Users");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");

        }


        public async Task<IActionResult> GiveAdminRights(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _userRepository.GiveAdminRightsAsync(id, role);
                    return RedirectToAction("GetAllUsers", "Users");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");

        }


        public async Task<IActionResult> TakeAdminRights(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _userRepository.TakeAdminRightsAsync(id, role);
                    return RedirectToAction("GetAllUsers", "Users");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");

        }
    }
}

