namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Common;
    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.ViewComponents.Countries;

    public class CountriesService : ICountriesService
    {
        private const int RequiredCountriesCount = 6;

        private readonly IRepository<Country> countriesRepository;
        private readonly IDeletableEntityRepository<Property> propertiesRepository;
        private readonly IRepository<PropertyImage> propertyImagesRepository;

        public CountriesService(
            IRepository<Country> countriesRepository,
            IDeletableEntityRepository<Property> propertiesRepository,
            IRepository<PropertyImage> propertyImagesRepository)
        {
            this.countriesRepository = countriesRepository;
            this.propertiesRepository = propertiesRepository;
            this.propertyImagesRepository = propertyImagesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairs()
        {
            return this.GetAllByFilter(c => c.Towns.Count != 0);
        }

        public IEnumerable<KeyValuePair<string, string>> GetMostPopularByKeyValuePairs()
        {
            return this.GetAllByFilter(
                c => c.Towns.Count != 0
                && c.Towns.Any(t => t.Properties.Any(p => p.Offers.Count > 0)));
        }

        public IEnumerable<CountryInListViewModel> GetMostPopular()
        {
            var countriesDb = this.countriesRepository
                .All()
                .AsQueryable();
            var propertiesDb = this.propertiesRepository
                .All()
                .AsQueryable();
            var imagesDb = this.propertyImagesRepository
                .All()
                .AsQueryable();
            var countries = new List<CountryInListViewModel>();
            foreach (var countryDb in countriesDb)
            {
                var propertiesCount = propertiesDb
                    .Where(p => p.Town.Country.Name == countryDb.Name && p.Offers.Count > 0)
                    .ToList()
                    .Count;
                if (propertiesCount == 0)
                {
                    continue;
                }

                var imageDb = imagesDb
                    .Where(p => p.Property.Town.Country.Name == countryDb.Name)
                    .FirstOrDefault();

                var image =
                    imageDb == null ?
                    GlobalConstants.DefaultImagePath
                    : $"{GlobalConstants.PropertyImagesPath}{imageDb.Id}.{imageDb.Extension}";

                var country = new CountryInListViewModel
                {
                    Name = countryDb.Name,
                    PropertiesCount = propertiesCount,
                    Image = image,
                };

                countries.Add(country);
            }

            int countriesCount = countries.Count();
            return countries
                .OrderByDescending(c => c.PropertiesCount)
                .Take(countriesCount >= RequiredCountriesCount ? RequiredCountriesCount : countriesCount);
        }

        public IEnumerable<string> GetMostPopularByNames()
        {
            return this.GetMostPopular()
                .Select(c => c.Name)
                .ToList();
        }

        private IEnumerable<KeyValuePair<string, string>> GetAllByFilter(Func<Country, bool> filter)
        {
            return this.countriesRepository.All()
                .Where(filter)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                })
                .OrderBy(c => c.Name)
                .ToList().Select(c => new KeyValuePair<string, string>(c.Id.ToString(), c.Name));
        }
    }
}
