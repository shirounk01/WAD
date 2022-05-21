using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WAD.Models
{
    public class BookNGoContext : IdentityDbContext<IdentityUser>
    {
        public BookNGoContext(DbContextOptions<BookNGoContext> options) : base(options){ }

        //public DbSet<User>? Users { get; set; }
        public DbSet<Review>? Reviews { get; set; }
        public DbSet<Hotel>? Hotels { get; set; }
        public DbSet<Flight>? Flights { get; set; }
        public DbSet<BookHotel>? BookHotels { get; set; }
        public DbSet<BookFlight>? BookFlights { get; set; }

    }
}

