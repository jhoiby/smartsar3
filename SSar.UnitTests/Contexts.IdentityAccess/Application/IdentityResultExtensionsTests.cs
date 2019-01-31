using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using SSar.Contexts.IdentityAccess.Application.Extensions;
using Xunit;

namespace SSar.UnitTests.Contexts.IdentityAccess.Application
{
    public class IdentityResultExtensionsTests
    {
        [Fact]
        public void ToCommandResult_should_add_notifications()
        {

            var identityResult = IdentityResult.Failed(
                new IdentityError { Code = "code1", Description = "description1A" },
                new IdentityError { Code = "code1", Description = "description1B" },
                new IdentityError { Code = "code2", Description = "description2" });

            var commandResult = identityResult.ToCommandResult();

            commandResult.ShouldSatisfyAllConditions(
                () => commandResult.Notifications.Count.ShouldBe(2),
                () => commandResult.Notifications["code1"].ShouldContain(n => n.Message.Contains("description1A"), 1),
                () => commandResult.Notifications["code1"].ShouldContain(n => n.Message.Contains("description1B"), 1),
                () => commandResult.Notifications["code2"].ShouldContain(n => n.Message.Contains("description2"), 1));
        }
    }
}
