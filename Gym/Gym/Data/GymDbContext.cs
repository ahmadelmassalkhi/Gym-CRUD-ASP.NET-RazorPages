using Gym.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Membership> Memberships { get; set; }
    }
}
