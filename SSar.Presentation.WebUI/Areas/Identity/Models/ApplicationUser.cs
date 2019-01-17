using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SSar.Presentation.WebUI.Areas.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public ApplicationUser() : base() { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
