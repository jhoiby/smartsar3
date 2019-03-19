using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SSar.Presentation.WebUI.Middleware;

namespace SSar.Presentation.WebUI.Middleware
{
    public class AzureADIdentityBridgeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AzureADIdentityBridgeMiddleware> _logger;

        private readonly string _loginProviderDisplayName = "SARCI"; // My organization's name
        private readonly string _targetExternalLoginPath = "/Identity/Account/ExternalLogin?returnUrl=%2F&handler=Callback";
        private readonly string _emailClaimName = "preferred_username";
        private readonly string _azureADCookieName = ".AspNetCore.AzureADCookie";

        public AzureADIdentityBridgeMiddleware(RequestDelegate next, ILogger<AzureADIdentityBridgeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // Part 2:
            // Catch an external-login redirect (from part 1 below) and convert the AzureAD cookie to an
            // Identity.External cookie so GetExternalLoginInfoAsync will work in ExternalLogin.OnGetCallbackAsync.
            
            if (context.Request.Path.ToString().Contains("aad-identity-bridge"))
            {
                _logger.LogDebug($"AzureADIdentityBridge: Handling redirect after oidc response.");

                if (!context.User.Identities.Any(x => x.IsAuthenticated))
                {
                    // Now that the AzureAD cookie has been returned in the request we can call AuthenticateAsync
                    // and get the claims, tickets, etc. to use to build the Identity.External cookie, which
                    // will then be used by ExternalLogin.OnGetCallbackAsync.
                    var aadAuthResult = await context.AuthenticateAsync(AzureADDefaults.AuthenticationScheme);

                    if (aadAuthResult?.Principal != null)
                    {
                        context.User = BuildExternalIdentityPrincipal(aadAuthResult);

                        _logger.LogDebug("AzureADIdentityBridge: Attempting signin with Identity cookie.");
                        await context.SignInAsync(IdentityConstants.ExternalScheme, context.User, aadAuthResult.Properties);
                        
                        if (context.Request.Cookies[_azureADCookieName] != null)
                        {
                            context.Response.Cookies.Delete(_azureADCookieName);
                        }
                    }
                }

                // Short-circuit the pipeline and redirect to the ExternalLogin callback handler
                context.Response.Redirect(context.Request.Query["OriginalUrl"]);
                return;
            }

            // If we didn't intercept a redirect, pass on execution to next middleware
            await _next(context);

            // Part 1:
            // On the way back up the middleware stack, catch the response from the oidc-callback and reroute it
            // to AADIdentityBridge (part 2, above). We need to do a full response/request cycle to give the AzureAD
            // cookie a chance to be saved in the browser and returned in the next request.

            // Review: Consider testing if response has an AzureAD cookie before redirecting.

            if (context.Response.Headers["Location"] == _targetExternalLoginPath
                    && CookieExists(context, _azureADCookieName))
            {
                _logger.LogDebug("AzureADIdentityBridge: Intercepted oidc callback response.");

                context.Response.Redirect("/aad-identity-bridge?" + "OriginalUrl=" +
                                          Uri.EscapeDataString(context.Response.Headers["Location"]));
            }
        }

        public ClaimsPrincipal BuildExternalIdentityPrincipal(AuthenticateResult aadAuthResult)
        {
            var principal = new ClaimsPrincipal();
            principal.AddIdentities(aadAuthResult.Principal.Identities);

            if (aadAuthResult?.Ticket?.Properties != null)
            {
                aadAuthResult.Ticket.Properties.Items[".AuthScheme"] 
                    = MicrosoftAccountDefaults.AuthenticationScheme;
                
                aadAuthResult.Ticket.Properties.Items["LoginProvider"]
                    = _loginProviderDisplayName;
            }

            // Add the email claim used by the Identity UI registration form. Note that in my specific case
            // I don't get an email claim from AzureAD, but instead use the preferred_username claim to
            // populate the email.

            var email = aadAuthResult.Principal.Claims.FirstOrDefault(c => c.Type == _emailClaimName)?.Value;
            if (email != null)
            {
                ((ClaimsIdentity)aadAuthResult.Principal.Identity).AddClaim(new Claim(ClaimTypes.Email, email));
            }

            return principal;
        }

        public bool CookieExists(HttpContext context, string cookieName)
        {
            foreach (var headers in context.Response.Headers.Values)
            { 
                foreach (var header in headers)
                {
                    if (header.StartsWith($"{cookieName}="))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }

    public static class AzureADIdentityBridgeExtensions
    {
        public static IApplicationBuilder UseAzureADIdentityBridge(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AzureADIdentityBridgeMiddleware>();
        }
    }
}

