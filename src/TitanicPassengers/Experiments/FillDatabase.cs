using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TitanicPassengers.AppDbContext;
using TitanicPassengers.Models;
using TitanicPassengers.Models.Enums;
using TitanicPassengers.Repositories;

namespace Experiments
{
	public class FillDatabase: IDisposable
    {
        private readonly AppDbContext _context;
        public UserRepository userRepository { get; set; }
        public ParticipantRepository participantRepository { get; set; }
        public PassengerRepository passengerRepository { get; set; }
        public ParticipantStatusRepository participantStatusRepository { get; set; }
        public LifeboatRepository lifeboatRepository { get; set; }
        public BodyRepository bodyRepository { get; set; }
        public CloseRelativeRepository closeRelativeRepository { get; set; }


        public FillDatabase()
		{
            var connectionString = "Host=localhost;Port=5432;Database=titanic_passengers_exp;User Id=postgres;Password=postgres;";

            var pgContextOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql(connectionString);
            _context = new AppDbContext(pgContextOptionsBuilder.Options);

            var contextFactory = new TestDbContextFactory(_context);

            userRepository = new UserRepository(contextFactory);
            participantRepository = new ParticipantRepository(contextFactory);
            participantStatusRepository = new ParticipantStatusRepository(contextFactory);
            passengerRepository = new PassengerRepository(contextFactory);
            lifeboatRepository = new LifeboatRepository(contextFactory);
            bodyRepository = new BodyRepository(contextFactory);
            closeRelativeRepository = new CloseRelativeRepository(contextFactory);
        }


        public void Dispose()
        {
            _context.Participants.RemoveRange(_context.Participants);
            _context.ParticipantStatuses.RemoveRange(_context.ParticipantStatuses);
            _context.Lifeboats.RemoveRange(_context.Lifeboats);
            _context.Bodies.RemoveRange(_context.Bodies);

            _context.SaveChanges();
        }



        public async Task AddParticipants(int count)
        {
            for (var i = 1; i < count + 1; i++)
            {
                await participantRepository.AddAsync(new Participant(i, "Name", "Surname", 50, Gender.Male, null), Role.Admin);
            }
        }


        public async Task AddLifeboats(int count)
        {
            for (var i = 1; i < count + 1; i++)
            {
                await lifeboatRepository.AddAsync(new Lifeboat(i, "", 50), Role.Admin);
            }
        }


        public async Task AddBodies(int count)
        {
            for (var i = 1; i < count + 1; i++)
            {
                await bodyRepository.AddAsync(new Body(i, "", DateOnly.MinValue), Role.Admin);
            }
        }


        public async Task AddStatuses(int count)
        {
            for (var i = 1; i < count + 1; i++)
            {
                await participantStatusRepository.AddAsync(new ParticipantStatus(i, Status.Survived, i % 100 + 1, i % 100 + 1), Role.Admin);
            }
        }


        public async Task<List<ParticipantStatus>> GetAllStatuses()
        {
            return await participantStatusRepository.GetAllAsync(Role.Admin);
        }


        public async Task<List<ParticipantStatus>> GetAllStatusesByLifeboat(int lifeboatId)
        {
            return await participantStatusRepository.GetByLifeboatAsync(lifeboatId, Role.Admin);
        }



        public async Task<Participant?> GetParticipantById(int id)
        {
            return await participantRepository.GetByIdAsync(id, Role.Admin);
            
        }


        public async Task RemoveAll()
        {
            //_context.Database.ExecuteSqlRaw("delete from participants_status");
            _context.ParticipantStatuses.RemoveRange(_context.ParticipantStatuses);
            //await _context.ParticipantStatuses.Cl
            await _context.SaveChangesAsync();
        }


        public async Task AddRangeParticipants(int count)
        {
            var list = new List<Participant>();
            for (var i = 1; i <= count; i++)
            {
                var participant = new Participant(i, "Name", "Surname", 50, Gender.Male, null);
                list.Add(participant);
            }
            await _context.Participants.AddRangeAsync(list);
            await _context.SaveChangesAsync();
            
        }


        public async Task AddRangeStatuses(int count)
        {
            var list = new List<ParticipantStatus>();
            for (var i = 1; i <= count; i++)
            {
                var participant = new ParticipantStatus(i, Status.Survived, i % 100 + 1, null);
                list.Add(participant);
            }

            await _context.ParticipantStatuses.AddRangeAsync(list);
            await _context.SaveChangesAsync();

        }
    }
}

