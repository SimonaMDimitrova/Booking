namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.ViewComponents.Countries;

    public class CountriesService : ICountriesService
    {
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
            return this.countriesRepository.All()
                .Where(c => c.Towns.Count != 0)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                })
                .OrderBy(c => c.Name)
                .ToList().Select(c => new KeyValuePair<string, string>(c.Id.ToString(), c.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetMostPopularByKeyValuePairs()
        {
            return this.countriesRepository.All()
                .Where(c => c.Towns.Count != 0 && c.Towns.Any(t => t.Properties.Any(p => p.Offers.Count > 0)))
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                })
                .OrderBy(c => c.Name)
                .ToList().Select(c => new KeyValuePair<string, string>(c.Id.ToString(), c.Name));
        }

        public IEnumerable<CountryInListViewModel> GetTheSixMostVisited()
        {
            var countriesDb = this.countriesRepository
                .All()
                .AsQueryable();
            var countries = new List<CountryInListViewModel>();

            foreach (var countryDb in countriesDb)
            {
                var propertiesCount = this.propertiesRepository
                    .All()
                    .Where(p => p.Town.Country.Name == countryDb.Name && p.Offers.Count > 0)
                    .ToList()
                    .Count;
                if (propertiesCount == 0)
                {
                    continue;
                }

                var imageDb = this.propertyImagesRepository
                    .All()
                    .Where(p => p.Property.Town.Country.Name == countryDb.Name)
                    .FirstOrDefault();

                var image =
                    imageDb == null ?
                    "assets/images/defaults/default.png"
                    : $"images/properties/{imageDb.Id}.{imageDb.Extension}";

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
                .Take(countriesCount >= 6 ? 6 : countriesCount);
        }

        public IEnumerable<string> GetTheSixMostVisitedNames()
        {
            return this.GetTheSixMostVisited()
                .Select(c => c.Name)
                .ToList();
        }
    }
}
