namespace Booking.Services.Data
{
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;

    public class CurrencyService : ICurrenciesService
    {
        private readonly IRepository<Country> countriesRepository;

        public CurrencyService(IRepository<Country> countriesRepository)
        {
            this.countriesRepository = countriesRepository;
        }

        public string GetCurrencyCodeByCountryId(int id)
        {
            return this.countriesRepository
                .AllAsNoTracking()
                .Where(c => c.Id == id)
                .Select(c => c.Currency.CurrencyCode)
                .First()
                .ToString();
        }
    }
}
