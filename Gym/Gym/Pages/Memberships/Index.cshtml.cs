using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gym.Models;

namespace Gym.Pages.Memberships
{
    public class IndexModel : PageModel
    {
        private readonly Gym.Data.GymDbContext _context;
        public IndexModel(Gym.Data.GymDbContext context)
        {
            _context = context;
            IsActive = true;
        }

        [BindProperty(SupportsGet = true)]
        public string FullNameSearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string PhoneNumberSearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool IsActive { get; set; }


        public IList<Membership> Memberships { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Memberships = await _context.Memberships.ToListAsync();

            if (!string.IsNullOrEmpty(FullNameSearchTerm))
            {
                Memberships = Memberships
                    .Where(m => m.FullName.Contains(FullNameSearchTerm))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(PhoneNumberSearchTerm))
            {
                Memberships = Memberships
                    .Where(m => m.PhoneNumber.Contains(PhoneNumberSearchTerm))
                    .ToList();
            }

            Memberships = Memberships
                .Where(m => m.IsActive() == IsActive)
                .ToList();
        }
    }
}
