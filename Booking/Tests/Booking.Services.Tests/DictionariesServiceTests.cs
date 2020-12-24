namespace Booking.Services.Data.Tests
{
    using System.Collections.Generic;

    using Booking.Services;
    using Booking.Web.ViewModels.BedTypes;
    using Booking.Web.ViewModels.Facilities;
    using Xunit;

    public class DictionariesServiceTests
    {
        private IDictionariesService service = new DictionariesService();
        private List<BedTypeViewModel> bedTypes = new List<BedTypeViewModel>
            {
                new BedTypeViewModel { Capacity = 1, Type = "Single bed" },
                new BedTypeViewModel { Capacity = 1, Type = "Single bed" },
                new BedTypeViewModel { Capacity = 1, Type = "Single bed" },
                new BedTypeViewModel { Capacity = 1, Type = "Single bed" },
                new BedTypeViewModel { Capacity = 2, Type = "Bedroom" },
                new BedTypeViewModel { Capacity = 2, Type = "Bedroom" },
                new BedTypeViewModel { Capacity = 2, Type = "Bunkbed" },
                new BedTypeViewModel { Capacity = 2, Type = "Bunkbed" },
            };

        private List<OfferFacilityViewModel> facilities = new List<OfferFacilityViewModel>
            {
                new OfferFacilityViewModel { Category = "First cat", Name = "First facility", },
                new OfferFacilityViewModel { Category = "First cat", Name = "Second facility", },
                new OfferFacilityViewModel { Category = "First cat", Name = "Third facility", },
                new OfferFacilityViewModel { Category = "Thrid cat", Name = "First facility", },
                new OfferFacilityViewModel { Category = "Thrid cat", Name = "Fifth facility", },
                new OfferFacilityViewModel { Category = "General", Name = "First general" },
            };

        [Fact]
        public void CheckCreateBedTypeWithElements()
        {
            var expectedResult = new Dictionary<string, int>()
            {
                { "Single bed", 4 },
                { "Bedroom", 4 },
                { "Bunkbed", 4 },
            };
            var actualResult = this.service.CreateBedTypes(this.bedTypes);

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void CheckCreateFacilitiesWithElements()
        {
            var expectedResult = new Dictionary<string, List<string>>()
            {
                { "First cat", new List<string>() { "First facility", "Second facility", "Third facility" } },
                { "Thrid cat", new List<string>() { "First facility", "Fifth facility" } },
                { "General", new List<string>() { "First general" } },
            };
            var actualResult = this.service.CreateFacilities(this.facilities);

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
