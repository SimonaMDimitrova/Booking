namespace Booking.Services.Data
{
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;

    public class CurrencyService : ICurrenciesService
    {
        private readonly IRepository<Country> countriesRepository;
        private readonly IRepository<Property> propertyRepository;

        public CurrencyService(
            IRepository<Country> countriesRepository,
            IRepository<Property> propertyRepository)
        {
            this.countriesRepository = countriesRepository;
            this.propertyRepository = propertyRepository;
        }

        public string GetCurrencyByPropertyId(string id)
        {
            return this.propertyRepository
                .All()
                .Where(p => p.Id == id)
                .Select(p => p.Town.Country.Currency.CurrencyCode)
                .FirstOrDefault();
        }

        public string GetCurrencyCodeByCountryId(int id)
        {
            return this.countriesRepository
                .All()
                .Where(c => c.Id == id)
                .Select(c => c.Currency.CurrencyCode)
                .First()
                .ToString();
        }
    }
}
