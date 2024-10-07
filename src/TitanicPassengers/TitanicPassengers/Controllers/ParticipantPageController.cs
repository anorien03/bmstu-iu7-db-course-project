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
        private readonly CloseRelativeRepository _closeRelativeRepository;
        public ParticipantPageController(ParticipantRepository participantRepository,
            ParticipantStatusRepository participantStatusRepository, PassengerRepository passengerRepository, LifeboatRepository lifeboatRepository,
            BodyRepository bodyRepository, CloseRelativeRepository closeRelativeRepository)
		{
			_participantRepository = participantRepository;
			_participantStatusRepository = participantStatusRepository;
			_passengerRepository = passengerRepository;
            _lifeboatRepository = lifeboatRepository;
            _bodyRepository = bodyRepository;
            _closeRelativeRepository = closeRelativeRepository;

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
                var model = new ParticipantPageViewModel() { Participant = participant, Role = role ?? Role.User };
                    
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
            return RedirectToAction("GetAllParticipants", "Participants");
        }


        [HttpGet]
        public async Task<IActionResult> UpdateParticipant(int id)
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
                        var participant = await _participantRepository.GetByIdAsync(id, role);
                        var model = new AddParticipantViewModel()
                        { Name = participant.Name, Surname = participant.Surname, Age = participant.Age, Gender = participant.Gender,
                            Position = participant.Position, Id = participant.Id, IsPassenger = participant.Position == null };

                        return View(model);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Data access layer");
                    }
                }
            }

            return RedirectToAction("GetParticipant", "ParticipantPage", new { id = id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateParticipant(AddParticipantViewModel model)
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
                        var participant = new Participant(model.Id ?? 0, model.Name, model.Surname, model.Age, model.Gender, position);
                        await _participantRepository.UpdateAsync(participant, role);

                        return RedirectToAction("GetParticipant", "ParticipantPage", new {id = model.Id});
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Data access layer");
                    }
                }
            }

            return View(model);

        }


        [HttpGet]
        public IActionResult AddPassenger(int id)
        {
            var model = new AddPassengerViewModel()
            { PassengerClass = PassengerClass.First, Departure = "", Destination = "", Id = id };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPassenger(AddPassengerViewModel model)
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
                        var passenger = new Passenger(model.Id ?? 0, model.Departure, model.Destination, model.PassengerClass);
                        await _passengerRepository.AddAsync(passenger, role);

                        return RedirectToAction("GetParticipant", "ParticipantPage", new { id = model.Id });
                    }
                    catch (InvalidDataException)
                    {
                        ModelState.AddModelError("", "Data access layer");
                        
                    }
                }
            }

            return View(model);

        }



        [HttpGet]
        public async Task<IActionResult> UpdatePassenger(int id)
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
                        var passenger = await _passengerRepository.GetByIdAsync(id, role);
                        var model = new AddPassengerViewModel()
                        {
                            PassengerClass = passenger.PassengerClass,
                            Departure = passenger.Departure,
                            Destination = passenger.Destination,
                            Id = passenger.ParticipantId
                        };

                        return View(model);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Data access layer");
                    }
                }
            }

            return RedirectToAction("GetParticipant", "ParticipantPage", new { id = id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassenger(AddPassengerViewModel model)
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
                        var passenger = new Passenger(model.Id ?? 0, model.Departure, model.Destination, model.PassengerClass);
                        await _passengerRepository.UpdateAsync(passenger, role);

                        return RedirectToAction("GetParticipant", "ParticipantPage", new { id = model.Id });
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Data access layer");
                    }
                }
            }

            return View(model);

        }


        public async Task<IActionResult> RemovePassenger(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _passengerRepository.RemoveAsync(id, role);
                    return RedirectToAction("GetParticipant", "ParticipantPage", new { id = id });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                }
            }

            return RedirectToAction("GetParticipant", "ParticipantPage", new { id = id });

        }



        [HttpGet]
        public IActionResult AddStatus(int id)
        {
            var model = new AddStatusViewModel()
            { Status = Status.Survived, Id = id };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStatus(AddStatusViewModel model)
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
                        var status = new ParticipantStatus(model.Id ?? 0, model.Status, model.LifeboatId, model.BodyId);
                        await _participantStatusRepository.AddAsync(status, role);

                        return RedirectToAction("GetParticipant", "ParticipantPage", new { id = model.Id });
                    }
                    catch (InvalidDataException e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
            }

            return View(model);

        }



        [HttpGet]
        public async Task<IActionResult> UpdateStatus(int id)
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
                        var status = await _participantStatusRepository.GetByIdAsync(id, role);
                        var model = new AddStatusViewModel()
                        {
                            Status = status.Status,
                            Id = status.ParticipantId
                        };

                        return View(model);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Data access layer");
                    }
                }
            }

            return RedirectToAction("GetParticipant", "ParticipantPage", new { id = id });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(AddStatusViewModel model)
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
                        var status = new ParticipantStatus(model.Id ?? 0, model.Status, model.LifeboatId, model.BodyId);
                        await _participantStatusRepository.UpdateAsync(status, role);

                        return RedirectToAction("GetParticipant", "ParticipantPage", new { id = model.Id });
                    }
                    catch (InvalidDataException e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Data access layer");
                    }
                }
            }

            return View(model);

        }


        public async Task<IActionResult> RemoveStatus(int id)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _participantStatusRepository.RemoveAsync(id, role);
                    return RedirectToAction("GetParticipant", "ParticipantPage", new { id = id });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                }
            }

            return RedirectToAction("GetParticipant", "ParticipantPage", new { id = id });

        }


        [HttpGet]
        public IActionResult AddRelative(int id)
        {
            var model = new AddRelativeViewModel()
            { Name = "", Surname = "", Id = id };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRelative(AddRelativeViewModel model)
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
                        var relative = await _closeRelativeRepository.GetByNameAsync(model.Name, model.Surname, role);
                        await _participantRepository.AddRelativeAsync(model.Id ?? 0, relative, role);

                        return RedirectToAction("GetParticipant", "ParticipantPage", new { id = model.Id });
                    }
                    catch (InvalidDataException e)
                    {
                        ModelState.AddModelError("", e.Message);
                    }
                }
            }

            return View(model);

        }


        public async Task<IActionResult> RemoveRelative(int id, int relativeId)
        {
            var user = HttpContext.User;
            var roleString = user.FindFirst("Role");
            if (user.Identity is not null && user.Identity.IsAuthenticated)
            {
                try
                {
                    Role? role = roleString != null ? Enum.Parse<Role>(roleString.Value) : null;
                    await _participantRepository.RemoveAsync(id, relativeId, role);
                    return RedirectToAction("GetParticipant", "ParticipantPage", new { id = id });
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Data Access layer");
                }
            }

            return RedirectToAction("GetParticipant", "ParticipantPage", new { id = id });

        }

    }
}

