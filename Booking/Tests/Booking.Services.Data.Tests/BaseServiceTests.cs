namespace Booking.Services.Data.Tests
{
    using System;

    using Booking.Data;
    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Data.Repositories;
    using Booking.Services.Messaging;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public abstract class BaseServiceTests
    {
        protected BaseServiceTests()
        {
            var services = this.SetServices();

            this.ServiceProvider = services.BuildServiceProvider();
            this.DbContext = this.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected IServiceProvider ServiceProvider { get; set; }

        protected ApplicationDbContext DbContext { get; set; }

        public void Dispose()
        {
            this.DbContext.Database.EnsureDeleted();
            this.SetServices();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(
                options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services
                 .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                 {
                     options.Password.RequireDigit = false;
                     options.Password.RequireLowercase = false;
                     options.Password.RequireUppercase = false;
                     options.Password.RequireNonAlphanumeric = false;
                     options.Password.RequiredLength = 6;
                 })
                 .AddEntityFrameworkStores<ApplicationDbContext>();

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            // Application services
            services.AddTransient(typeof(ILogger<>), typeof(Logger<>));
            services.AddTransient(typeof(ILoggerFactory), typeof(LoggerFactory));
            services.AddTransient<Messaging.IEmailSender, NullMessageSender>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<ITownsService, TownsService>();
            services.AddTransient<ICurrenciesService, CurrenciesService>();
            services.AddTransient<IPropertyCategoriesService, PropertyCategoriesService>();
            services.AddTransient<IPropertiesService, PropertiesService>();
            services.AddTransient<IFacilitiesService, FacilitiesService>();
            services.AddTransient<IRulesService, RulesService>();
            services.AddTransient<IOffersService, OffersService>();
            services.AddTransient<IBedTypesService, BedTypesService>();
            services.AddTransient<IBookingsService, BookingsService>();

            var context = new DefaultHttpContext();
            services.AddSingleton<IHttpContextAccessor>(new HttpContextAccessor { HttpContext = context });

            return services;
        }
    }
}
