using System;
using Microsoft.AspNetCore.Identity;

namespace SSar.Domain.IdentityAuth.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {

        public ApplicationUser() : base() { }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
