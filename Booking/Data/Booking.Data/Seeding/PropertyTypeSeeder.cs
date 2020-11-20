namespace Booking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Models;

    internal class PropertyTypeSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PropertyTypes.Any())
            {
                return;
            }

            await dbContext.PropertyTypes.AddAsync(new PropertyType { Name = "Apartment" });
            await dbContext.PropertyTypes.AddAsync(new PropertyType { Name = "Home" });
        }
    }
}
