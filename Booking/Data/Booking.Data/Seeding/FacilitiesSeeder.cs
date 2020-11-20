namespace Booking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Models;

    internal class FacilitiesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Facilities.Any())
            {
                return;
            }

            await dbContext.Facilities.AddAsync(new Facility { Name = "Air conditioning", FacilityCategoryId = 1 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Heating", FacilityCategoryId = 1 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Free WiFi", FacilityCategoryId = 1 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Free on-site parking", FacilityCategoryId = 1 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Electric vehicle charging station", FacilityCategoryId = 1 });

            await dbContext.Facilities.AddAsync(new Facility { Name = "Kitchen", FacilityCategoryId = 2 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Kitchenette", FacilityCategoryId = 2 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Washing machine", FacilityCategoryId = 2 });

            await dbContext.Facilities.AddAsync(new Facility { Name = "Flat-screen TV", FacilityCategoryId = 3 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Swimming pool", FacilityCategoryId = 3 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Hot tub", FacilityCategoryId = 3 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Minibar", FacilityCategoryId = 3 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Sauna", FacilityCategoryId = 3 });

            await dbContext.Facilities.AddAsync(new Facility { Name = "Balcony", FacilityCategoryId = 4 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Garden view", FacilityCategoryId = 4 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Terrace", FacilityCategoryId = 4 });
            await dbContext.Facilities.AddAsync(new Facility { Name = "View", FacilityCategoryId = 4 });
        }
    }
}
