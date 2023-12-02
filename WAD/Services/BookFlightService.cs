using WAD.Models;
using WAD.Repositories.Interfaces;
using WAD.Services.Interfaces;

namespace WAD.Services
{
    public class BookFlightService : IBookFlightService
    {
        private readonly IRepositoryWrapper _repo;
        private readonly IFlightService _flightService;

        public BookFlightService(IRepositoryWrapper repo, IFlightService flightService)
        {
            _repo = repo;
            _flightService = flightService;
        }

        public void BookFlight(string userGuid, Flight flight)
        {
            
            BookFlight bookFlight = new BookFlight();
            bookFlight.Flight = flight;
            bookFlight.UserGuid = userGuid;
            bookFlight.RegistrationDate = DateTime.Now;

            _repo.FlightRepository.Update(flight);
            _repo.BookFlightRepository.Create(bookFlight);

        }

        public void BookFlights(int goingId, int comingId, string userGuid)
        {
            Flight goingFlight = _flightService.GetFlightById(goingId);
            Flight comingFlight = _flightService.GetFlightById(comingId);
            BookFlight(userGuid, goingFlight);
            BookFlight(userGuid, comingFlight);

            _repo.Save();
        }

        public List<BookFlight> GetReservationsByUserId(string userGuid)
        {
            List<BookFlight> flightReservations = _repo.BookFlightRepository.FindByCondition(flight => flight.UserGuid == userGuid).ToList();
            return flightReservations;
        }
    }
}
