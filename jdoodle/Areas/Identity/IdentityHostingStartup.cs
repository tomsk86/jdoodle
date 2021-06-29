using jdoodle.Areas.Identity.Data;
using jdoodle.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(jdoodle.Areas.Identity.IdentityHostingStartup))]

namespace jdoodle.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<JDoodleDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("jdoodleDbContextConnection")));

                services.AddDefaultIdentity<JDoodleUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddEntityFrameworkStores<JDoodleDbContext>();
            });
        }
    }
}