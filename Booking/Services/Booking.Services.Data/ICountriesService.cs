namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Services.Data.Models;

    public interface ICountriesService
    {
        IEnumerable<CountryOfferAndBookingsCountDto> GetTheSixTopCountries();

        IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairs();
    }
}
