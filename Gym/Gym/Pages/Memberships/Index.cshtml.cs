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
        }

        [BindProperty(SupportsGet = true)]
        public string FullNameSearchTerm { get; set; }


        [BindProperty(SupportsGet = true)]
        public string PhoneNumberSearchTerm { get; set; }


        [BindProperty(SupportsGet = true)]
        public string IsActiveFilter { get; set; }  // Use string to handle "Any", "true", and "false"

        public IList<Membership> Memberships { get; set; } = default!;

        public async Task OnGetAsync()
        {
            var query = _context.Memberships.AsQueryable();

            if (!string.IsNullOrEmpty(FullNameSearchTerm))
            {
                query = query.Where(m => m.FullName.Contains(FullNameSearchTerm));
            }

            if (!string.IsNullOrEmpty(PhoneNumberSearchTerm))
            {
                query = query.Where(m => m.PhoneNumber.Contains(PhoneNumberSearchTerm));
            }

            if (!string.IsNullOrEmpty(IsActiveFilter))
            {
                if(IsActiveFilter == "true") query = query.Where(m => m.RegisterDate < m.ExpireDate);
                else query = query.Where(m => m.RegisterDate >= m.ExpireDate);
            }

            Memberships = await query.ToListAsync();
        }
    }
}
