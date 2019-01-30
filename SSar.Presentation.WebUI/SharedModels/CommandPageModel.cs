using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using SSar.Contexts.Common.Application.Commands;
using SSar.Contexts.Common.Domain.Notifications;

namespace SSar.Presentation.WebUI.Bases
{
    public class CommandPageModel : PageModel
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());

        public IActionResult ReturnNotificationsOrRedirectToPage(CommandResult commandResult, string pageName = "Index")
        {
            IActionResult pageAction = RedirectToPage(pageName);

            if (!ModelState.IsValid)
            {
                pageAction = Page();
            }

            if (!commandResult.Succeeded)
            {
                // TODO: Handle case of returned exception
                AddNotificationsToModelErrors(commandResult.Notifications);
                pageAction = Page();
            }

            return pageAction;
        }
        
        // TODO: **** REMOVE DEPENDENCY on Contexts.Common.Domain (i.e. NotificationList) ?
        private void AddNotificationsToModelErrors(NotificationList notifications)
        {
            foreach (var key in notifications)
            {
                var notificationList = key.Value;
                foreach (var notification in notificationList)
                {
                    ModelState.AddModelError(key.ToString(), notification.Message);
                }
            }
        }
    }
}
