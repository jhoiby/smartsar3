using System;
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
        public CreateExamplePersonCommand Data { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CommandResult result = await Mediator.Send(Data);

            // Result.AggregateId will contain the Id of the just-created ExamplePerson

            return RedirectToPage("Index");
        }
    }
}