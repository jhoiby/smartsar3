using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSar.Contexts.Membership.Application.Commands;
using SSar.Presentation.WebUI.SharedModels;

namespace SSar.Presentation.WebUI.Areas.Membership.Pages.MemberOrgs
{
    public class CreateModel : CommandPageModel
    {
        [BindProperty]
        public CreateMemberOrganizationCommand Command { get; set; }

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