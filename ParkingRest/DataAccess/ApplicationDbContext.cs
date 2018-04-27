using Microsoft.EntityFrameworkCore;
using ParkingRest.Models;

namespace ParkingRest.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<CarPark> CarParks { get; set; }
    }
}
