using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Booking.Web.Areas.Identity.IdentityHostingStartup))]

namespace Booking.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
