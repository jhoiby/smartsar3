using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSar.Contexts.Common.Notifications;
using SSar.Contexts.Membership.Application.Commands;
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
            Debug.WriteLine("\n\nDisplaying page \n\n\n");

            return Page();
        }
    }
}