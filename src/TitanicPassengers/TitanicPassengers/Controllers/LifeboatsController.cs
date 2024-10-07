using System;
using Microsoft.AspNetCore.Mvc;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;
using TitanicPassengers.Repositories;
using TitanicPassengers.ViewModels;

namespace TitanicPassengers.Controllers
{
	public class LifeboatsController: Controller
	{
        private readonly LifeboatRepository _lifeboatRepository;
        public LifeboatsController(LifeboatRepository lifeboatRepository)
        {
            _lifeboatRepository = lifeboatRepository;

        }


        public async Task<IActionResult> GetAllLifeboats()
        {
            var model = new LifeboatsViewModel();

            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    model.Lifeboats = await _lifeboatRepository.GetAllAsync(role);
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


        public async Task<IActionResult> RemoveLifeboat(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _lifeboatRepository.RemoveAsync(id, role);
                    return RedirectToAction("GetAllLifeboats", "Lifeboats");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");

        }


        [HttpGet]
        public IActionResult AddLifeboat()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLifeboat(AddLifeboatViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.User;
                var roleString = user.FindFirst("Role");


                if (user.Identity is not null && user.Identity.IsAuthenticated && roleString is not null)
                {
                    try
                    {

                        Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                        var lifeboat = new Lifeboat(model.Number, model.Boat, model.MaxCount);
                        await _lifeboatRepository.AddAsync(lifeboat, role);

                        return RedirectToAction("GetAllLifeboats", "Lifeboats");
                    }
                    catch (InvalidDataException e)
                    {
                        ModelState.AddModelError("", e.Message);
                        return View(model);
                    }
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");

        }


        [HttpGet]
        public async Task<IActionResult> UpdateLifeboat(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");


            if (user.Identity is not null && user.Identity.IsAuthenticated && roleString is not null)
            {
                try
                {

                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    var lifeboat = await _lifeboatRepository.GetByIdAsync(id, role);
                    var model = new AddLifeboatViewModel()
                    {
                        Number = lifeboat.Id,
                        Boat = lifeboat.Boat,
                        MaxCount = lifeboat.MaxCount,
                        Id = lifeboat.Id
                    };

                    return View(model);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data access layer");
                    return RedirectToAction("GetAllLifeboats", "Lifeboats");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLifeboat(AddLifeboatViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = HttpContext.User;
                var roleString = user.FindFirst("Role");


                if (user.Identity is not null && user.Identity.IsAuthenticated && roleString is not null)
                {
                    try
                    {

                        Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                        var lifeboat = new Lifeboat(model.Id ?? 0, model.Boat, model.MaxCount);
                        await _lifeboatRepository.UpdateAsync(lifeboat, role);

                        return RedirectToAction("GetAllLifeboats", "Lifeboats");
                    }
                    catch (InvalidDataException e)
                    {
                        ModelState.AddModelError("", e.Message);
                        return View(model);
                    }
                }
            }
            return View(model);

        }
    }
}

