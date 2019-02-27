using System.Runtime.Serialization;
using System.Security.Claims;
using FluentValidation.AspNetCore;
using HtmlTags;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
                    microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:Password"];
                });
                //.AddGoogle(o =>
                //{
                //    o.ClientId = Configuration["Authentication:Google:ClientId"];
                //    o.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                //    o.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                //    o.ClaimActions.Clear();
                //    o.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                //    o.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                //    o.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                //    o.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                //    o.ClaimActions.MapJsonKey("urn:google:profile", "link");
                //    o.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                //});

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
            UserManager<ApplicationUser> userManager)
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
            app.UseAuthentication();
            // If the app uses Session or TempData based on Session:
            // app.UseSession();
            app.UseMvc();
        }
    }
}
