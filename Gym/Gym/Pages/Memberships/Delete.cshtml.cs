using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Gym.Data;
using Gym.Models;
using Gym.Services.Helpers;

namespace Gym.Pages.Memberships
{
    public class DeleteModel : PageModel
    {
        private readonly Gym.Data.GymDbContext _context;

        public DeleteModel(Gym.Data.GymDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membership = await _context.Memberships.FirstOrDefaultAsync(m => m.Id == id);

            if (membership == null)
            {
                return NotFound();
            }
            else
            {
                Membership = membership;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membership = await _context.Memberships.FindAsync(id);
            if (membership != null)
            {
                Membership = membership;
                _context.Memberships.Remove(Membership);
                await _context.SaveChangesAsync();

                // set popup message
                TempData["PopupMessage"] = HtmlHelper.WrapInRedSpan(
                    $"Membership {HtmlHelper.HighlightText(Membership.FullName)} deleted successfully!"
                );
            }

            return RedirectToPage("./Index");
        }
    }
}
