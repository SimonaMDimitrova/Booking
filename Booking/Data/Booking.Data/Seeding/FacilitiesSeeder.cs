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

            var generalId = dbContext.FacilityCategories.FirstOrDefault(x => x.Name == "General").Id;
            var cookingAndCleaningId = dbContext.FacilityCategories.FirstOrDefault(x => x.Name == "Cooking and cleaning").Id;
            var entertainmentId = dbContext.FacilityCategories.FirstOrDefault(x => x.Name == "Entertainment").Id;
            var outsideAndViewId = dbContext.FacilityCategories.FirstOrDefault(x => x.Name == "Outside and view ").Id;

            await dbContext.Facilities.AddAsync(new Facility { Name = "Air conditioning", FacilityCategoryId = dbContext.FacilityCategories.FirstOrDefault(x => x.Name == "General").Id });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Heating", FacilityCategoryId = generalId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Free WiFi", FacilityCategoryId = generalId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Free on-site parking", FacilityCategoryId = generalId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Electric vehicle charging station", FacilityCategoryId = generalId });

            await dbContext.Facilities.AddAsync(new Facility { Name = "Kitchen", FacilityCategoryId = cookingAndCleaningId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Kitchenette", FacilityCategoryId = cookingAndCleaningId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Washing machine", FacilityCategoryId = cookingAndCleaningId });

            await dbContext.Facilities.AddAsync(new Facility { Name = "Flat-screen TV", FacilityCategoryId = entertainmentId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Swimming pool", FacilityCategoryId = entertainmentId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Hot tub", FacilityCategoryId = entertainmentId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Minibar", FacilityCategoryId = entertainmentId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Sauna", FacilityCategoryId = entertainmentId });

            await dbContext.Facilities.AddAsync(new Facility { Name = "Balcony", FacilityCategoryId = outsideAndViewId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Garden view", FacilityCategoryId = outsideAndViewId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "Terrace", FacilityCategoryId = outsideAndViewId });
            await dbContext.Facilities.AddAsync(new Facility { Name = "View", FacilityCategoryId = outsideAndViewId });
        }
    }
}
