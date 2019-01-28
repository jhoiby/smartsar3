using System;
using System.Collections.Generic;
using System.Text;
using SSar.Contexts.Common.Application.Authorization;

namespace SSar.Infrastructure.Authorization
{
    public class SimpleAuthorizationService : IAuthorizationService
    {
        public AuthorizationResult Authorize()
        {
            return AuthorizationResult.Authorized;
        }
    }
}
