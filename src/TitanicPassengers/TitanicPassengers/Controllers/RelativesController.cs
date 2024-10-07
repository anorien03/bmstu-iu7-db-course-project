using System;
using Microsoft.AspNetCore.Mvc;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;
using TitanicPassengers.Repositories;
using TitanicPassengers.ViewModels;

namespace TitanicPassengers.Controllers
{
	public class RelativesController: Controller
	{
        private readonly CloseRelativeRepository _closeRelativeRepository;
        public RelativesController(CloseRelativeRepository closeRelativeRepository)
        {
            _closeRelativeRepository = closeRelativeRepository;

        }


        public async Task<IActionResult> GetAllRelatives()
        {
            var model = new RelativesViewModel();

            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    model.Relatives = await _closeRelativeRepository.GetAllAsync(role);
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


        public async Task<IActionResult> RemoveRelative(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _closeRelativeRepository.RemoveAsync(id, role);
                    return RedirectToAction("GetAllRelatives", "Relatives");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");

        }


        [HttpGet]
        public IActionResult AddRelative()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRelative(NewRelativeViewModel model)
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
                        var relative = new CloseRelative(model.Name, model.Surname, model.Birthday, model.Gender);
                        Console.WriteLine($"\n\n\n\n\n{relative.Id}");
                        await _closeRelativeRepository.AddAsync(relative, role);

                        return RedirectToAction("GetAllRelatives", "Relatives");
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
        public async Task<IActionResult> UpdateRelative(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");


            if (user.Identity is not null && user.Identity.IsAuthenticated && roleString is not null)
            {
                try
                {

                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    var relative = await _closeRelativeRepository.GetByIdAsync(id, role);
                    var model = new NewRelativeViewModel()
                    {
                        Name = relative.Name,
                        Surname = relative.Surname,
                        Gender = relative.Gender,
                        Birthday = relative.Birthday,
                        Id = relative.Id
                    };

                    return View(model);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data access layer");
                    return RedirectToAction("GetAllRelatives", "Relatives");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateRelative(NewRelativeViewModel model)
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
                        var relative = new CloseRelative(model.Id ?? 0, model.Name, model.Surname, model.Birthday, model.Gender);
                        await _closeRelativeRepository.UpdateAsync(relative, role);

                        return RedirectToAction("GetAllRelatives", "Relatives");
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

