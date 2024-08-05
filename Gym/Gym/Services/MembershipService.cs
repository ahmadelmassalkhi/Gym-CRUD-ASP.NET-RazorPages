using Gym.Data;
using Gym.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.Services
{
    public class MembershipService : IMembershipService
    {
        public GymDbContext _context;
        public MembershipService(GymDbContext context)
        {
            _context = context;
        }

        public async Task<List<Membership>> GetMembershipsAsync()
        {
            return await _context.Memberships.ToListAsync();
        }

        public async Task AddMembershipAsync(Membership M)
        {
            // add & commit changes
            _context.Memberships.Add(M);
            await _context.SaveChangesAsync();
        }
    }
}
