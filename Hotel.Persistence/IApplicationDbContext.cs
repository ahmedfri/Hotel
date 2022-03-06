using Microsoft.EntityFrameworkCore;
using Hotel.Domain.Entities;
using System.Threading.Tasks;

namespace Hotel.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entities.Hotel> Hotels { get; set; }
        DbSet<HotelImage> HotelImages { get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Review> Reviews { get; set; }

        Task<int> SaveChangesAsync();
    }
}
