using HotelListing.Features.V1.Hotels;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class ProjectDB : DbContext
    {
        public ProjectDB(DbContextOptions<ProjectDB> options) : base(options)
        {

        }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
