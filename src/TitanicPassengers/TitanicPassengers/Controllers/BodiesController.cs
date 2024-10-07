using System;
using Microsoft.AspNetCore.Mvc;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;
using TitanicPassengers.Repositories;
using TitanicPassengers.ViewModels;

namespace TitanicPassengers.Controllers
{
	public class BodiesController: Controller
	{
        private readonly BodyRepository _bodyRepository;
        public BodiesController(BodyRepository bodyRepository)
        {
            _bodyRepository = bodyRepository;

        }


        public async Task<IActionResult> GetAllBodies()
        {
            var model = new BodiesViewModel();

            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    model.Bodies = await _bodyRepository.GetAllAsync(role);
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


        public async Task<IActionResult> RemoveBody(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _bodyRepository.RemoveAsync(id, role);
                    return RedirectToAction("GetAllBodies", "Bodies");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");

        }


        [HttpGet]
        public IActionResult AddBody()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBody(AddBodyViewModel model)
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
                        var body = new Body(model.Number, model.Boat, model.Date);
                        await _bodyRepository.AddAsync(body, role);

                        return RedirectToAction("GetAllBodies", "Bodies");
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
        public async Task<IActionResult> UpdateBody(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");


            if (user.Identity is not null && user.Identity.IsAuthenticated && roleString is not null)
            {
                try
                {

                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    var body = await _bodyRepository.GetByIdAsync(id, role);
                    var model = new AddBodyViewModel()
                    {
                        Number = body.Id,
                        Boat = body.Boat,
                        Date = body.Date,
                        Id = body.Id
                    };

                    return View(model);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data access layer");
                    return RedirectToAction("GetAllBodies", "Bodies");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateBody(AddBodyViewModel model)
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
                        var body = new Body(model.Id ?? 0, model.Boat, model.Date);
                        await _bodyRepository.UpdateAsync(body, role);

                        return RedirectToAction("GetAllBodies", "Bodies");
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

