using Gym.Models;

namespace Gym.Services
{
    public interface IMembershipService
    {
        public Task AddMembershipAsync(Membership M);
    }
}
