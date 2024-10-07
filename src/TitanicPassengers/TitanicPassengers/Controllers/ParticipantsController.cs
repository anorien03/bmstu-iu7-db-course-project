using System;
using System.Data;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;
using TitanicPassengers.Repositories;
using TitanicPassengers.ViewModels;

namespace TitanicPassengers.Controllers
{
	public class ParticipantsController: Controller
	{
		private readonly ParticipantRepository _participantRepository;
        public ParticipantsController(ParticipantRepository participantRepository)
		{
			_participantRepository = participantRepository;
		}


		public async Task<IActionResult> GetAllParticipants(Status? status)
		{
            var model = new ParticipantsListViewModel();

            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    model.Participants = await _participantRepository.GetAllAsync(role, status);
                    model.Role = role;
                    return View(model);
                }
                catch (InvalidCastException)
                {
                    ModelState.AddModelError("", "Data Access layer");
                    return View();
                }
            }

            try
            {
                model.Participants = await _participantRepository.GetAllAsync(null);

                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Data Access layer");
                return View();
            }

        }


        public async Task<IActionResult> RemoveParticipant(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _participantRepository.RemoveAsync(id, role);
                    return RedirectToAction("GetAllParticipants", "Participants");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                    return RedirectToAction("GetAllParticipants", "Participants");
                }
            }

            return RedirectToAction("GetAllParticipants", "Participants");

        }


        [HttpGet]
        public IActionResult AddParticipant()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddParticipant(AddParticipantViewModel model)
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
                        var position = model.IsPassenger ? null : model.Position;
                        var participant = new Participant(model.Name, model.Surname, model.Age, model.Gender, position);
                        await _participantRepository.AddAsync(participant, role);

                        return RedirectToAction("GetAllParticipants", "Participants");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Data access layer");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You need to log in at first");
                }
            }

            return View(model);

        }



    }
}

