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

            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Hotel", PropertyTypeId = 1, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Guesthouse", PropertyTypeId = 2, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Hostel", PropertyTypeId = 1, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Condo hotel", PropertyTypeId = 1, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Capsule Hotel", PropertyTypeId = 1, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Country House", PropertyTypeId = 2, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Farm stay", PropertyTypeId = 2, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Love Hotel", PropertyTypeId = 1, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Motel", PropertyTypeId = 1, });
            await dbContext.PropertyCategories.AddAsync(new PropertyCategory { Name = "Lodge", PropertyTypeId = 2, });
        }
    }
}
