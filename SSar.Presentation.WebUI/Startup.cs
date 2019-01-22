using FluentValidation.AspNetCore;
using HtmlTags;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSar.Application.Commands;
using SSar.Application.Commands.Membership;
using SSar.Data;
using SSar.Domain.IdentityAuth.Entities;
using SSar.Infrastructure.DomainEvents;
using SSar.Infrastructure.ServiceBus;

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

            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ApplicationId"];
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:Password"];
            });
            
            services.AddHtmlTags();
            
            // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>)); // Not currently in use

            services.AddMediatR(typeof(CreateExamplePersonCommandHandler).Assembly);
            
            services.AddTransient<IServiceBusSender, ServiceBusSenderAzure>((ctx) =>
                new SSar.Infrastructure.ServiceBus.ServiceBusSenderAzure(
                    new TopicClient(Configuration["AzureServiceBus:ServiceBusConnectionString"],
                        Configuration["AzureServiceBus:Topic"])));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<CreateExamplePersonCommandValidator>();
                });

            services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            AppDbContext identityDbContext, 
            RoleManager<ApplicationRole> roleManager, 
            UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                DummyData.Initialize(identityDbContext, userManager, roleManager).Wait();
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

            app.UseMvc();
        }
    }
}
