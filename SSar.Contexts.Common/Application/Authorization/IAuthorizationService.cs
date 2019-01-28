using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Application.Authorization
{
    public interface IAuthorizationService
    {
        AuthorizationResult Authorize();
    }
}
