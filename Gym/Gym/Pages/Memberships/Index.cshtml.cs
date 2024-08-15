using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gym.Models;
using Gym.Services.Helpers;

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
                if(IsActiveFilter == "true") query = query.Where(m => DateTime.Now < m.ExpireDate);
                else query = query.Where(m => DateTime.Now >= m.ExpireDate);
            }

            Memberships = await query.ToListAsync();
        }


        public IActionResult OnPostToggleStatus(int id)
        {
            Console.WriteLine("HEREEE");

            var membership = _context.Memberships.Find(id);
            if (membership == null) return NotFound();

            if(!membership.IsActive())
            {
                // Activate Membership
                membership.RegisterDate = DateTime.Now;
                membership.ExpireDate = DateTime.Now.AddMonths(1);
            } else
            {
                // Deactivate Membership
                membership.ExpireDate = DateTime.Now;
            }

            // Save changes to database
            _context.SaveChanges();

            // Prepare popup message
            if(membership.IsActive())
            {
                TempData["PopupMessage"] = HtmlHelper.WrapInGreenSpan(
                    $"Membership {HtmlHelper.HighlightText(membership.FullName)} activated successfully!"
                );
            }
            else
            {
                TempData["PopupMessage"] = HtmlHelper.WrapInRedSpan(
                    $"Membership {HtmlHelper.HighlightText(membership.FullName)} deactivated successfully!"
                );
            }

            // return to page
            return RedirectToPage();
        }
    }
}
