namespace Booking.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Offers.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.Edit;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class FacilitiesServiceTests : BaseServiceTests
    {
        private IFacilitiesService Service => this.ServiceProvider.GetRequiredService<IFacilitiesService>();

        private IDeletableEntityRepository<PropertyFacility> PropertyFacilityRepository => this.ServiceProvider.GetRequiredService<IDeletableEntityRepository<PropertyFacility>>();

        private IRepository<Facility> FacilityRepository => this.ServiceProvider.GetRequiredService<IRepository<Facility>>();

        [Fact]
        public void CheckGetAllByPropertyIdCategoryMethod()
        {
            var property = this.GetPropertyAddedToDb();

            var exectedResult = new List<PropertyFacilityInputModel>();
            foreach (var propertyFacility in property.PropertyFacilities)
            {
                exectedResult.Add(new PropertyFacilityInputModel
                {
                    IsChecked = this.PropertyFacilityRepository
                    .All()
                    .Any(p => p.PropertyId == property.Id && p.Facility.Name == propertyFacility.Facility.Name),
                    Name = propertyFacility.Facility.Name,
                    Id = propertyFacility.FacilityId,
                });
            }

            var actualResult = this.Service.GetAllByPropertyId(property.Id).ToList();

            Assert.Equal(exectedResult.Count, actualResult.Count);
            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(exectedResult[i].Name, actualResult[i].Name);
                Assert.Equal(exectedResult[i].Id, actualResult[i].Id);
                Assert.Equal(exectedResult[i].IsChecked, actualResult[i].IsChecked);
            }
        }

        [Fact]
        public void GetAllExceptInGeneralCategory()
        {
            this.GetPropertyAddedToDb();
            var facilities = this.FacilityRepository
                .All()
                .Where(f => f.FacilityCategory.Name != "General")
                .ToList();

            var expectedResult = new List<OfferFacilityInputModel>();
            foreach (var facility in facilities)
            {
                expectedResult.Add(new OfferFacilityInputModel
                {
                    Category = facility.FacilityCategory.Name,
                    Id = facility.Id,
                    Name = facility.Name,
                });
            }

            var actualResult = this.Service.GetAllExeptInGeneralCategory().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            for (int i = 0; i < actualResult.Count; i++)
            {
                Assert.Equal(expectedResult[i].Category, actualResult[i].Category);
                Assert.Equal(expectedResult[i].Id, actualResult[i].Id);
                Assert.Equal(expectedResult[i].Name, actualResult[i].Name);
            }
        }

        private Property GetPropertyAddedToDb()
        {
            var property = new Property
            {
                Stars = 5,
                Address = "some address",
                ApplicationUserId = Guid.NewGuid().ToString(),
                Floors = 5,
                Description = "some desc",
                Name = "My hotel",
                Town = new Town
                {
                    Country = new Country
                    {
                        Currency = new Currency
                        {
                            CurrencyCode = "CRR",
                        },
                        Name = "Some country",
                    },
                    Name = "Some town",
                },
                PropertyCategory = new PropertyCategory
                {
                    Name = "Cat",
                    PropertyType = new PropertyType { Name = "Hotel" },
                },
                PropertyFacilities =
                {
                    new PropertyFacility { Facility = new Facility { Name = "Facility1", FacilityCategory = new FacilityCategory { Name = "General" } } },
                },
                Offers =
                {
                    new Offer
                    {
                        Count = 2,
                        PricePerPerson = 50,
                        OfferBedTypes = { new OfferBedType { BedType = new BedType { Type = "Bunkbed", Capacity = 2 } } },
                        ValidFrom = DateTime.UtcNow,
                        ValidTo = DateTime.UtcNow.AddDays(10),
                        OfferFacilities =
                        {
                            new OfferFacility { Facility = new Facility { Name = "Facility1", FacilityCategory = new FacilityCategory { Name = "Cat1" } } },
                            new OfferFacility { Facility = new Facility { Name = "Facility1", FacilityCategory = new FacilityCategory { Name = "Cat2" } } },
                            new OfferFacility { Facility = new Facility { Name = "Facility1", FacilityCategory = new FacilityCategory { Name = "Cat3" } } },
                            new OfferFacility { Facility = new Facility { Name = "Facility1", FacilityCategory = new FacilityCategory { Name = "Cat4" } } },
                        },
                    },
                },
            };

            this.DbContext.Properties.Add(property);
            this.DbContext.SaveChanges();

            return property;
        }
    }
}
