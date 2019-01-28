using System;
using System.Collections.Generic;
using System.Text;
using Shouldly;
using SSar.Contexts.Common.Application.Authorization;
using SSar.Infrastructure.Authorization;
using Xunit;

namespace SSar.UnitTests.Infrastructure.Authorization
{
    public class SimpleAuthorizationServiceTests
    {
        [Fact]
        public void Should_be_assignable_to_IAuthorizationService()
        {
            var service = new SimpleAuthorizationService();

            service.ShouldBeAssignableTo<IAuthorizationService>();
        }

        [Fact]
        public void Should_return_authorized()  // Temporarily fixed to authorized for development
        {
            var service = new SimpleAuthorizationService();

            var result = service.Authorize();

            result.ShouldBe(AuthorizationResult.Authorized);
        }
    }
}
