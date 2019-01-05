using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSar.Contexts.Membership.Application.Commands;
using SSar.Presentation.WebUI.Bases;

namespace SSar.Presentation.WebUI.Areas.Membership.Pages.ExamplePerson
{
    public class CreateModel : CommandPageModel
    {
        [BindProperty]
        public CreateExamplePersonCommand Data { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = await Mediator.Send(Data);

            // Result.Data will contain the Guid of the just-created ExamplePerson

            return RedirectToPage("Index");
        }
    }
}