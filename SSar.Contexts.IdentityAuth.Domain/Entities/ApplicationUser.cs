using Microsoft.AspNetCore.Identity;
using System;

namespace SSar.Contexts.IdentityAuth.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public ApplicationUser() : base() { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
