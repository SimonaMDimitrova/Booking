namespace Booking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Models;

    internal class BedTypesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.BedTypes.Any())
            {
                return;
            }

            await dbContext.BedTypes.AddAsync(new BedType { Type = "Single bed", Capacity = 1 });
            await dbContext.BedTypes.AddAsync(new BedType { Type = "Bedroom bed", Capacity = 2 });
            await dbContext.BedTypes.AddAsync(new BedType { Type = "Sofa bed", Capacity = 2 });
            await dbContext.BedTypes.AddAsync(new BedType { Type = "Bunk bed", Capacity = 2 });
        }
    }
}
