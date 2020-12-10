namespace Booking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Models;

    internal class PropertyCategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.PropertyCategories.Any())
            {
                return;
            }

            var apartmentId = dbContext.PropertyTypes.FirstOrDefault(x => x.Name == "Apartment").Id;
            var homeId = dbContext.PropertyTypes.FirstOrDefault(x => x.Name == "Home").Id;

            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Hotel", PropertyTypeId = apartmentId, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Guesthouse", PropertyTypeId = homeId, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Hostel", PropertyTypeId = apartmentId, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Condo hotel", PropertyTypeId = apartmentId, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Capsule Hotel", PropertyTypeId = apartmentId, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Country House", PropertyTypeId = homeId, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Farm stay", PropertyTypeId = homeId, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Love Hotel", PropertyTypeId = apartmentId, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Motel", PropertyTypeId = apartmentId, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Lodge", PropertyTypeId = homeId, });
        }
    }
}
