using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSar.Contexts.Common.Notifications;

namespace SSar.Presentation.WebUI.Infrastructure.Helpers
{
    public static class CommandResultExtensions
    {
        public static ModelStateDictionary ToModelStateDictionary(this CommandResult cmdResult)
        {
            var modelStateDict = new ModelStateDictionary();

            foreach (var notification in cmdResult.Notifications)
            {
                modelStateDict.AddModelError(notification.ForField, notification.Message);
            }

            return modelStateDict;
        }
    }
}
