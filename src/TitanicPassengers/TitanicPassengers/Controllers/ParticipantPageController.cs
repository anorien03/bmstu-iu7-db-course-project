using System;
using Microsoft.AspNetCore.Mvc;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;
using TitanicPassengers.Repositories;
using TitanicPassengers.ViewModels;

namespace TitanicPassengers.Controllers
{
	public class ParticipantPageController: Controller
	{
		private readonly ParticipantRepository _participantRepository;
        private readonly ParticipantStatusRepository _participantStatusRepository;
        private readonly PassengerRepository _passengerRepository;
        private readonly LifeboatRepository _lifeboatRepository;
        private readonly BodyRepository _bodyRepository;
        public ParticipantPageController(ParticipantRepository participantRepository,
            ParticipantStatusRepository participantStatusRepository, PassengerRepository passengerRepository, LifeboatRepository lifeboatRepository, BodyRepository bodyRepository)
		{
			_participantRepository = participantRepository;
			_participantStatusRepository = participantStatusRepository;
			_passengerRepository = passengerRepository;
            _lifeboatRepository = lifeboatRepository;
            _bodyRepository = bodyRepository;

        }


		public async Task<IActionResult> GetParticipant(int id)
		{
            

            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;

                    var participant = await _participantRepository.GetByIdAsync(id, role);
                    if (participant == null)
                        return RedirectToAction("GetAllParticipants", "Participants");
                    Console.WriteLine($"\n\n\n\n\n\n{participant.Id}");
                var model = new ParticipantPageViewModel() { Participant = participant };
                    
                    model.Passenger = model.Participant.Passenger;
                    model.Relatives = model.Participant.CloseRelatives;
                    model.Status = model.Participant.ParticipantStatus;
                    if (model.Status != null)
                    {
                        if (model.Status.LifeboatId != null)
                            model.Lifeboat = await _lifeboatRepository.GetByIdAsync(model.Status.LifeboatId ?? 0, role);

                        if (model.Status.BodyId != null)
                            model.Body = await _bodyRepository.GetByIdAsync(model.Status.BodyId ?? 0, role);
                    }


                    return View(model);
                }
                catch (InvalidCastException)
                {
                    ModelState.AddModelError("", "Data Access layer");
                    
                }
            }
            return View();
            //return RedirectToAction("GetAllParticipants", "Participants");
        }
	}
}

