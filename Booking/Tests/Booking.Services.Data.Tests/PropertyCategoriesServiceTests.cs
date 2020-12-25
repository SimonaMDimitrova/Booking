namespace Booking.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Booking.Data.Models;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class PropertyCategoriesServiceTests : BaseServiceTests
    {
        private IPropertyCategoriesService Service => this.ServiceProvider.GetRequiredService<IPropertyCategoriesService>();

        [Fact]
        public void CheckGetAllByKeyValuePairsMethod()
        {
            var propertyCategories = this.GetCreatedPropertyCategories();

            var expectedResult = new List<KeyValuePair<string, string>>();
            foreach (var category in propertyCategories)
            {
                expectedResult.Add(new KeyValuePair<string, string>(category.Id.ToString(), category.Name));
            }

            var actualResult = this.Service.GetAllByKeyValuePairs().ToList();

            Assert.Equal(expectedResult.Count, actualResult.Count);
            Assert.Equal(expectedResult, actualResult);
        }

        private IEnumerable<PropertyCategory> GetCreatedPropertyCategories()
        {
            var propertyType = new PropertyType { Name = "Apartment", };
            var propertyCategories = new List<PropertyCategory>();
            for (int i = 0; i < 6; i++)
            {
                propertyCategories.Add(new PropertyCategory { Name = $"Hotel{i}", PropertyTypeId = propertyType.Id });
            }

            this.DbContext.AddRange(propertyCategories);
            this.DbContext.SaveChanges();

            return propertyCategories;
        }
    }
}
