using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using SSar.Contexts.IdentityAuth.Application.Extensions;
using Xunit;

namespace SSar.UnitTests.Contexts.IdentityAccess.Application
{
    public class IdentityResultExtensionsTests
    {
        [Fact]
        public void ToCommandResult_should_add_notifications()
        {

            var identityResult = IdentityResult.Failed(
                new IdentityError { Code = "code1", Description = "description1" }, 
                new IdentityError { Code = "code2", Description = "description2" });

            var commandResult = identityResult.ToCommandResult();

            // TODO: Finish this once CommandResult is finished
            throw new NotImplementedException("Finish once CommandResult is ready.");
        }
    }
}
