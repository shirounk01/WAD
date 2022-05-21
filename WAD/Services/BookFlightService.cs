using WAD.Models;
using WAD.Repositories.Interfaces;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class BookFlightService : IBookFlightService
    {
        private IRepositoryWrapper _repo;
        private IFlightService _flightService;

        public BookFlightService(IRepositoryWrapper repo, IFlightService flightService)
        {
            _repo = repo;
            _flightService = flightService;
        }

        public void BookFlight(User user, Flight flight)
        {
            
            BookFlight bookFlight = new BookFlight();
            bookFlight.Flight = flight;
            bookFlight.User = user;
            bookFlight.RegistrationDate = DateTime.Now;

            _repo.FlightRepository.Update(flight);
            _repo.BookFlightRepository.Create(bookFlight);

        }

        public void BookFlights(int goingId, int comingId)
        {
            User user = _repo.UserRepository.FindAll().FirstOrDefault();
            Flight goingFlight = _flightService.GetFlightById(goingId);
            Flight comingFlight = _flightService.GetFlightById(comingId);
            BookFlight(user, goingFlight);
            BookFlight(user, comingFlight);

            _repo.UserRepository.Update(user);

            _repo.Save();
        }
    }
}
