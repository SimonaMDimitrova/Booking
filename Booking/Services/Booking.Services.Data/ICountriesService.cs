namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.ViewComponents.Countries;

    public interface ICountriesService
    {
        IEnumerable<CountryInListViewModel> GetTheSixMostVisited();

        IEnumerable<string> GetTheSixMostVisitedNames();

        IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairs();
    }
}
