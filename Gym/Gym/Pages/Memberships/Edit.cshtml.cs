using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gym.Models;
using Gym.Services.Helpers;

namespace Gym.Pages.Memberships
{
    public class EditModel : PageModel
    {
        private readonly Gym.Data.GymDbContext _context;

        public EditModel(Gym.Data.GymDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var membership =  await _context.Memberships.FirstOrDefaultAsync(m => m.Id == id);
            if (membership == null) return NotFound();

            Membership = membership;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            _context.Attach(Membership).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

                // setup Popup message
                TempData["PopupMessage"] = HtmlHelper.WrapInGreenSpan(
                    $"Membership {HtmlHelper.HighlightText(Membership.FullName)} updated successfully!"
                );
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MembershipExists(Membership.Id)) return NotFound();
                else throw;
            }

            return RedirectToPage("./Index");
        }

        private bool MembershipExists(int id)
        {
            return _context.Memberships.Any(e => e.Id == id);
        }
    }
}
