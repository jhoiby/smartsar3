using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SSar.Presentation.WebUI.Areas.Identity.Models;
using SSar.Presentation.WebUI.Data;

[assembly: HostingStartup(typeof(SSar.Presentation.WebUI.Areas.Identity.IdentityHostingStartup))]
namespace SSar.Presentation.WebUI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}