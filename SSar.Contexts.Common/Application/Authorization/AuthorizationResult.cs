using System;
using System.Collections.Generic;
using System.Text;

namespace SSar.Contexts.Common.Application.Authorization
{
    /// <summary>
    /// Result of an authorization test. If rules do not explicitly allow or
    /// deny authorization the result is returned as Unspecified and the
    /// application may decide how to handle. A rule returning Denied
    /// takes precedence over any other rules.
    /// </summary>
    public enum AuthorizationResult
    {
        Authorized,
        Unspecified,
        Denied
    }
}
