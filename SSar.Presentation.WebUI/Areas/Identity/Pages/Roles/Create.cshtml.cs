using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSar.Contexts.IdentityAuth.Application.Commands;
using SSar.Presentation.WebUI.Bases;

namespace SSar.Presentation.WebUI.Areas.Identity.Pages.Roles
{
    public class CreateModel : CommandPageModel
    {
        public CreateModel()
        {
        }

        [BindProperty]
        public CreateRoleCommand Command { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await Mediator.Send(Command);

            return RedirectToPage("Index");
        }
    }
}