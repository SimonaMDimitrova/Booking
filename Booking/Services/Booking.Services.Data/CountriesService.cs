namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Services.Data.Models;

    public class CountriesService : ICountriesService
    {
        private readonly IRepository<Country> countriesRepository;

        public CountriesService(IRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairs()
        {
            var countries = this.countriesRepository.All()
                .Where(c => c.Towns.Count != 0)
                .Select(c => new
                {
                    c.Id,
                    c.Name,
                })
                .OrderBy(c => c.Name)
                .ToList().Select(c => new KeyValuePair<string, string>(c.Id.ToString(), c.Name));

            return countries;
        }

        public IEnumerable<CountryOfferAndBookingsCountDto> GetTheSixTopCountries()
        {
            var countries = this.countriesRepository
                .All()
                .ToList()
                .Select(c => new CountryOfferAndBookingsCountDto
                {
                    Name = c.Name,
                    OffersCount = c.Towns.Sum(t => t.Properties.Sum(p => p.Offers.Count)),
                    BookingsCount = c.Towns.Sum(t => t.Properties.Sum(p => p.ApplicationUser.ApplicationUserOffers.Count)),
                })
                .OrderBy(c => c.OffersCount)
                .ThenBy(c => c.BookingsCount)
                .Take(6)
                .ToList();

            return countries;
        }
    }
}
