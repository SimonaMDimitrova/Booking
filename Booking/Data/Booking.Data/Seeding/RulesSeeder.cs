namespace Booking.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Models;

    internal class RulesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Rules.Any())
            {
                return;
            }

            await dbContext.Rules.AddAsync(new Rule { Name = "Smoking" });
            await dbContext.Rules.AddAsync(new Rule { Name = "Pets" });
            await dbContext.Rules.AddAsync(new Rule { Name = "Children" });
            await dbContext.Rules.AddAsync(new Rule { Name = "Parties" });
        }
    }
}
