namespace Booking.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class BedTypesServiceTests : BaseServiceTests
    {
        private IBedTypesService Service => this.ServiceProvider.GetRequiredService<IBedTypesService>();

        [Fact]
        public void CheckGetAllMethod()
        {
            var firstBedType = new BedType
            {
                Type = "Single bed",
                Capacity = 1,
            };

            var secondBedType = new BedType
            {
                Type = "Bunkbed",
                Capacity = 2,
            };

            this.DbContext.BedTypes.Add(firstBedType);
            this.DbContext.BedTypes.Add(secondBedType);
            this.DbContext.SaveChanges();

            var expectedResult = new List<KeyValuePair<int, string>>()
            {
                new KeyValuePair<int, string>(2, "Bunkbed"),
                new KeyValuePair<int, string>(1, "Single bed"),
            };
            var actualResult = this.Service.GetAll();

            Assert.Equal(expectedResult, actualResult.ToList());
            Assert.Equal(2, actualResult.Count());
        }
    }
}
