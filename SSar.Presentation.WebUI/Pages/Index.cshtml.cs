using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SSar.Presentation.WebUI.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            Debug.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
