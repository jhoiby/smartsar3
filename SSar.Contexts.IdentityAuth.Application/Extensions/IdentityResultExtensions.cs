using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SSar.Contexts.Common.Application.Commands;

namespace SSar.Contexts.IdentityAuth.Application.Extensions
{
    public static class IdentityResultExtensions
    {
        public static CommandResult ToCommandResult(this IdentityResult identityResult)
        {
            return new CommandResult();
        }
    }
}
