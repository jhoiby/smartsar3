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
using SSar.Presentation.WebUI.Configuration;

namespace SSar.Presentation.WebUI.Configuration
{
    public class AzureADIdentityBridgeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AzureADIdentityBridgeMiddleware> _logger;

        public AzureADIdentityBridgeMiddleware(RequestDelegate next, ILogger<AzureADIdentityBridgeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // TODO: Fix brittle code. If AAD cookie doesn't get deleted it will intercept and log in
            // with other OIDC callback types such as Google Auth

            // Part 2:
            // Catch an external-login redirect and convert the AzureAD cookie to an Identity.External cookie

            if (context.Request.Path.ToString().Contains("aad-identity-bridge"))
            {
                _logger.LogDebug($"AADIdentityBridge: Received redirect after oidc response.");

                if (!context.User.Identities.Any(x => x.IsAuthenticated))
                {
                    var aadAuthResult = await context.AuthenticateAsync(AzureADDefaults.AuthenticationScheme);

                    if (aadAuthResult?.Principal != null)
                    {
                        // Definitely an AzureAD sign in callback, so go for it: build and save an Identity.External
                        // cookie. See Microsoft.AspNetCore.Authentication.RemoteAuthenticationHandler
                        // (tag Release 2.2.3, line 139) for clues on how to set up the SignIn parameters.

                        var principal = new ClaimsPrincipal();
                        principal.AddIdentities(aadAuthResult.Principal.Identities);
                        context.User = principal;
                        
                        // TODO: Why doesn't the registration form auto-populate the email for AAD users?

                        if (aadAuthResult?.Ticket?.Properties != null)
                        {
                            aadAuthResult.Ticket.Properties.Items[".AuthScheme"] // Determines auth/cookie handler
                                = MicrosoftAccountDefaults.AuthenticationScheme;
                            aadAuthResult.Ticket.Properties.Items["LoginProvider"] // Display name and db key
                                = "Azure AD";
                        }

                        _logger.LogDebug("AzureAD Auth Helper: Attempting signin with Identity cookie.");
                        await context.SignInAsync(IdentityConstants.ExternalScheme, principal, aadAuthResult.Properties);

                        // TODO: Get rid of magic string. Find way to programmatically get name of cookie from AspNetCore
                        // in case it changes in the future.
                        if (context.Request.Cookies[".AspNetCore.AzureADCookie"] != null)
                        {
                            context.Response.Cookies.Delete(".AspNetCore.AzureADCookie");
                        }
                    }
                }

                // Short-circuit the pipleine and redirect to the ExternalLogin callback handler
                var originalLocation = context.Request.Query["Url"];
                context.Response.Redirect(originalLocation);
                return;
            }

            // If we didn't intercept a redirect, pass on execution to next middleware
            await _next(context);

            // Part 1:
            // Catch response from oidc-callback and reroute it to AADIdentityBridge
            // (We need to do the full response/request cycle to give let the cookie settle.)

            if (context.Response.Headers["Location"] ==
                    "/Identity/Account/ExternalLogin?returnUrl=%2F&handler=Callback")
            {
                var originalLocation = context.Response.Headers["Location"];
                context.Response.Redirect("/aad-identity-bridge?" + "Url=" +
                                          Uri.EscapeDataString(originalLocation));
            }
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

