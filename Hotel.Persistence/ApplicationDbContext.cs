using Microsoft.EntityFrameworkCore;
using Hotel.Domain.Entities;
using System.Threading.Tasks;
namespace Hotel.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        // This constructor is used of runit testing
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

     
        public DbSet<Domain.Entities.Hotel> Hotels { get ; set ; }
        public DbSet<Room> Rooms { get ; set ; }
        public DbSet<HotelImage> HotelImages { get ; set ; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set ; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                .UseSqlServer("DataSource=app.db");
            }

        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
