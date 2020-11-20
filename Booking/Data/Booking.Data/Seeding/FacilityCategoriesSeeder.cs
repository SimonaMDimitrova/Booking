namespace Booking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Models;

    internal class FacilityCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.FacilityCategories.Any())
            {
                return;
            }

            await dbContext.FacilityCategories.AddAsync(new FacilityCategory { Name = "General" });
            await dbContext.FacilityCategories.AddAsync(new FacilityCategory { Name = "Cooking and cleaning" });
            await dbContext.FacilityCategories.AddAsync(new FacilityCategory { Name = "Entertainment" });
            await dbContext.FacilityCategories.AddAsync(new FacilityCategory { Name = "Outside and view" });
        }
    }
}
