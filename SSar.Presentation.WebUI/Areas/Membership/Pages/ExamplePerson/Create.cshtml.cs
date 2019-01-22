using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSar.Application.Commands;
using SSar.Application.Commands.Membership;
using SSar.Presentation.WebUI.Bases;

namespace SSar.Presentation.WebUI.Areas.Membership.Pages.ExamplePerson
{
    public class CreateModel : CommandPageModel
    {
        [BindProperty]
        public CreateExamplePersonCommand Command { get; set; }

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