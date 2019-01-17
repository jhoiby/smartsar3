using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;
using SSar.Contexts.Common.Notifications;

namespace SSar.Presentation.WebUI.Bases
{
    public class CommandPageModel : PageModel
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ?? (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        
        //public IActionResult NotificationsToPageOrRedirectIfOk(CommandResult cmdResult, string page = "Index")
        //{
        //    if (cmdResult.HasNotifications)
        //    {
        //        foreach (var notification in cmdResult.Notifications)
        //        {
        //            ModelState.AddModelError(notification.ForField, notification.Message);
        //        }

        //        return Page();
        //    }

        //    return RedirectToPage(page);
        //}
    }
}
