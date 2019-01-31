using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSar.Contexts.Common.Domain.Notifications;
using SSar.Contexts.Membership.Application.Commands;
using SSar.Presentation.WebUI.SharedModels;

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
            return 
                ReturnNotificationsOrRedirectToPage(
                    await Mediator.Send(Command), 
                    "Index");
        }
    }
}