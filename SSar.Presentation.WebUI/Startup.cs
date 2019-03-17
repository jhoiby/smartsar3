using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using Dapper;
using FluentValidation.AspNetCore;
using HtmlTags;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SSar.Contexts.Common.Application.IntegrationEvents;
using SSar.Contexts.Common.Application.RequestPipelineBehaviors;
using SSar.Contexts.Common.Application.ServiceInterfaces;
using SSar.Contexts.Common.Data;
using SSar.Contexts.Common.Data.Outbox;
using SSar.Contexts.Common.Data.ServiceInterfaces;
using SSar.Contexts.Common.Domain.ServiceInterfaces;
using SSar.Contexts.IdentityAccess.Application.Commands;
using SSar.Contexts.IdentityAccess.Domain.Entities;
using SSar.Contexts.Membership.Application.Commands;
using SSar.Infrastructure.Authorization;
using SSar.Infrastructure.DomainEventDispatch;
using SSar.Infrastructure.IntegrationEventQueues;
using SSar.Infrastructure.ServiceBus;
using SSar.Presentation.WebUI.Infrastructure.Tags;
using SSar.Presentation.WebUI.Services;
using IAuthorizationService = SSar.Contexts.Common.Application.Authorization.IAuthorizationService;

namespace SSar.Presentation.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ApplicationSqlDbConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication()
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
                    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:Password"];
                })
                .AddGoogle(o =>
                {
                    o.ClientId = Configuration["Authentication:Google:ClientId"];
                    o.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                    o.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                    o.ClaimActions.Clear();
                    o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                    o.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                    o.ClaimActions.MapJsonKey("urn:google:profile", "link");
                    o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                })
                .AddAzureAD(options =>
                {
                    Configuration.Bind("Authentication:AzureAd", options);

                });

            services.AddHtmlTags(new TagConventions());
            
            services.AddTransient(typeof(RequestPreProcessorBehavior<,>),
                typeof(RequestAuthorizationBehavior<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), 
                typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), 
                typeof(RequestLoggerBehavior<,>));
            services.AddMediatR(
                typeof(CreateExamplePersonCommandHandler).Assembly,
                typeof(CreateRoleCommandHandler).Assembly,
                typeof(Areas.Identity.Pages.Roles.IndexModel).Assembly);

            services.AddTransient<IQueryService, SqlDbQueryService>(ctx =>
                new SqlDbQueryService(
                    Configuration.GetConnectionString("ApplicationSqlDbConnection")));

            services.AddSingleton<IIntegrationEventQueue, IntegrationEventQueue>();

            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

            services.AddTransient<IOutboxService, OutboxService>();
            
            services.AddScoped<IAuthorizationService, SimpleAuthorizationService>();

            services.AddTransient<IServiceBusSender<IIntegrationEvent>, 
                ServiceBusSenderAzure<IIntegrationEvent>>((ctx) =>
                new ServiceBusSenderAzure<IIntegrationEvent>(
                    new TopicClient(
                        Configuration["AzureServiceBus:ServiceBusConnectionString"],
                        Configuration["AzureServiceBus:Topic"]),
                    new AzureIntegrationMessageBuilder()));

            services.AddMvc(config =>
                {
                    var policy = new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build();
                    config.Filters.Add(new AuthorizeFilter(policy));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CreateExamplePersonCommandValidator>();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env,
            AppDbContext appDbContext, 
            RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                DummyData.Initialize(appDbContext, userManager, roleManager).Wait();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.Use(async (context, next) =>
            {
                logger.LogDebug("      Middleware debug point 1");
                await next.Invoke();
                logger.LogDebug("      Middleware debug point 8");

                var test1 = context.User.Identity.IsAuthenticated;
                var test2 = context.User.Claims.Count();
                var test3 = context.User.Identity.AuthenticationType;
                logger.LogDebug($"          User.IsAuthenticated = {test1.ToString()}");
                logger.LogDebug($"          User.Claims.Count = {test2.ToString()}");
                logger.LogDebug($"          User.Identity.AuthenticationType = {test3}");

                var authResultTest1 = await context.AuthenticateAsync(AzureADDefaults.AuthenticationScheme);
                logger.LogDebug($"          AzureAD AuthResult.Succeeded = {authResultTest1.Succeeded.ToString()}");

                //
                //
                // TODO: Try to perform a SignIn(Identity.External) here to save an identity cookie.
                //
                // Will need to build the parameters for the SignIn method. I may have to extract the ticket the same way OIDC does?
                // See Microsoft.AspNetCore.Authentication.RemoteAuthenticationHandler:139 for clues on how to set up this SignIn.
                //
                // await context.SignInAsync(IdentityConstants.ExternalScheme, principal, authResult1.Properties);
                //

            });


            app.UseAuthentication();

            app.Use(async (context, next) =>
            {
                logger.LogDebug("      Middleware debug point 2");
                await next.Invoke();
                logger.LogDebug("      Middleware debug point 7");
            });

            app.Use(async (context, next) =>
            {
                logger.LogDebug("AzureAD Auth Helper: middleware execution starting.");

                // If not authenticated, attempt AzureAD authentication/conversion
                if (!context.User.Identities.Any(x => x.IsAuthenticated))
                {
                    var principal = new ClaimsPrincipal();

                    // Get AzureAD authenticate info
                    
                    var authResult1 = await context.AuthenticateAsync(AzureADDefaults.AuthenticationScheme);
                    var authResult2 = await context.AuthenticateAsync(IdentityConstants.ExternalScheme);

                    if (authResult2?.Ticket?.Properties != null)
                    {
                        authResult2.Ticket.Properties.Items[".AuthScheme"] = "Microsoft";
                        authResult2.Ticket.Properties.Items["LoginProvider"] = "Microsoft";
                    }



                    if (authResult1?.Principal != null)
                    {
                        // Build ClaimsPrincipal (user)
                        principal.AddIdentities(authResult1.Principal.Identities);
                        context.User = principal;

                        // Persist info for Identity.External authenticate handler to use

                        // See Microsoft.AspNetCore.Authentication.RemoteAuthenticationHandler:139 for clues on how to set up this SignIn

                        if (authResult1?.Ticket?.Properties != null)
                        {
                            authResult1.Ticket.Properties.Items[".AuthScheme"] = "Microsoft";
                            authResult1.Ticket.Properties.Items["LoginProvider"] = "Microsoft";
                        }


                        logger.LogDebug("AzureAD Auth Helper: Attempting signin with Identity cookie.");
                        await context.SignInAsync(IdentityConstants.ExternalScheme, principal, authResult1.Properties);

                        // TO TRY: GET COOKIE FROM REPLY AND PUT IT IN CURRENT REQUEST?
                        // THIS APPEARS TO BE THE PROBLEM!!!!

                        var cookieTest = context.Request.Cookies;
                        
                        logger.LogDebug("AzureAD Auth Helper: Attempting test AuthenticateAsync(Identity.External).");
                        var authResultTest = await context.AuthenticateAsync(IdentityConstants.ExternalScheme);
                        if (authResultTest.Succeeded)
                        {
                            logger.LogDebug("    - Authenticate test succeeded.");
                        }
                        else
                        {
                            logger.LogDebug("    - Authenticate test failed.");
                        }
                        var cookiesTest = context.Request.Cookies;
                    }
                }
                else
                {
                    // Tests for Google comparison

                    var authResultG = await context.AuthenticateAsync(IdentityConstants.ExternalScheme);

                    var test1 = context.Request.Cookies;
                }

                logger.LogDebug("AzureAD Auth Helper: Invoking next middleware.");


                logger.LogDebug("AzureAD Auth Helper: Calling next.Invoke().");
                await next.Invoke();
                logger.LogDebug("AzureAD Auth Helper: Returned from next.Invoke()");


                logger.LogDebug("AzureAD Auth Helper: Execution complete");
            });
            
            app.Use(async (context, next) =>
            {
                logger.LogDebug("      Middleware debug point 3");
                await next.Invoke();
                logger.LogDebug("      Middleware debug point 6");
            });

            app.UseMvc();



            app.Use(async (context, next) =>
            {
                logger.LogDebug("      Middleware debug point 4");
                await next.Invoke();
                logger.LogDebug("      Middleware debug point 5");
            });
        }
    }
}
