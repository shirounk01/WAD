using WAD.Models;
using WAD.Repositories.Interfaces;

namespace WAD.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly BookNGoContext _bookNGoContext;
        private IUserRepository? _userRepository;
        private IFlightRepository? _flightRepository;
        private IBookFlightRepository? _bookFlightRepository;
        private IHotelRepository? _hotelRepository;
        private IBookHotelRepository? _bookHotelRepository;
        private IReviewRepository? _reviewRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_bookNGoContext);
                }

                return _userRepository;
            }
        }

        public IFlightRepository FlightRepository
        {
            get
            {
                if (_flightRepository == null)
                {
                    _flightRepository = new FlightRepository(_bookNGoContext);
                }

                return _flightRepository;
            }
        }
        public IBookFlightRepository BookFlightRepository
        {
            get
            {
                if (_bookFlightRepository == null)
                {
                    _bookFlightRepository = new BookFlightRepository(_bookNGoContext);
                }

                return _bookFlightRepository;
            }
        }
        public IHotelRepository HotelRepository
        {
            get
            {
                if (_hotelRepository == null)
                {
                    _hotelRepository = new HotelRepository(_bookNGoContext);
                }

                return _hotelRepository;
            }
        }

        public IBookHotelRepository BookHotelRepository
        {
            get
            {
                if (_bookHotelRepository == null)
                {
                    _bookHotelRepository = new BookHotelRepository(_bookNGoContext);
                }

                return _bookHotelRepository;
            }
        }
        public IReviewRepository ReviewRepository
        {
            get
            {
                if (_reviewRepository == null)
                {
                    _reviewRepository = new ReviewRepository(_bookNGoContext);
                }

                return _reviewRepository;
            }
        }
        public RepositoryWrapper(BookNGoContext context)
        {
            _bookNGoContext = context;
        }

        public void Save()
        {
            _bookNGoContext.SaveChanges();
        }
    }
}
