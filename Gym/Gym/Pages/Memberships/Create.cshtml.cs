using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Gym.Data;
using Gym.Models;
using Gym.Services;
using System;
using Gym.Services.Helpers;

namespace Gym.Pages.Memberships
{
    public class CreateModel : PageModel
    {
        private readonly GymDbContext _context;
        private readonly IMembershipService _membershipService;

        [BindProperty]
        public Membership Membership { get; set; } = default!;

        public CreateModel(GymDbContext context, IMembershipService membershipService)
        {
            _context = context;
            _membershipService = membershipService;

            // initialize membership
            Membership = new()
            {
                RegisterDate = DateTime.Today,
                ExpireDate = DateTime.Today.Date.AddMonths(1)
            };
        }

        public IActionResult OnGet() => Page();

        public async Task<IActionResult> OnPostAsync()
        {
            // validate date
            /*
             if (Membership.RegisterDate >= Membership.ExpireDate)
            {
                ModelState.AddModelError(
                    string.Empty, 
                    "Register date must be earlier than expire date.");
            }
             */

            if (!ModelState.IsValid) return Page();

            // set popup message
            TempData["PopupMessage"] = HtmlHelper.WrapInGreenSpan(
                $"Membership {HtmlHelper.HighlightText(Membership.FullName)} created successfully!"
            );

            await _membershipService.AddMembershipAsync(Membership);
            return RedirectToPage("./Index");
        }
    }
}
