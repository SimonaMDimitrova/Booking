namespace Booking.Services.Data.Tests
{
    using System.Collections.Generic;

    using Booking.Services;
    using Booking.Services.Models;
    using Booking.Web.InputModels.Offers.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.ById;
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
        public void CheckCreateBedTypeMethod()
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
        public void CheckCreateFacilitiesMethod()
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

        [Fact]
        public void CheckCreateFacilitiesInputMethod()
        {
            List<OfferFacilityInputModel> facilities = new List<OfferFacilityInputModel>
            {
                new OfferFacilityInputModel { Id = 1, Category = "First cat", Name = "First facility", },
                new OfferFacilityInputModel { Id = 2, Category = "Thrid cat", Name = "First facility", },
                new OfferFacilityInputModel { Id = 3, Category = "General", Name = "First general" },
            };

            var expectedResult = new Dictionary<string, List<FacilityIdNameServiceModel>>()
            {
                { "First cat", new List<FacilityIdNameServiceModel> { new FacilityIdNameServiceModel { Id = 1, Name = "First facility" } } },
                { "Thrid cat", new List<FacilityIdNameServiceModel> { new FacilityIdNameServiceModel { Id = 2, Name = "First facility" } } },
                { "General", new List<FacilityIdNameServiceModel> { new FacilityIdNameServiceModel { Id = 3, Name = "First general" } } },
            };

            var actualResult = this.service.CreateFacilitiesInput(facilities);

            Assert.Equal(expectedResult.Count, actualResult.Count);
            foreach (var result in actualResult)
            {
                Assert.True(expectedResult.ContainsKey(result.Key));
                for (int i = 0; i < result.Value.Count; i++)
                {
                    var actual = result.Value[i];
                    var expected = expectedResult[result.Key][i];

                    Assert.Equal(expected.Id, actual.Id);
                    Assert.Equal(expected.Name, actual.Name);
                }
            }
        }
    }
}
