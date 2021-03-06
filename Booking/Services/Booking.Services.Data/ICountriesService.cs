﻿namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.ViewComponents.Countries;

    public interface ICountriesService
    {
        IEnumerable<CountryInListViewModel> GetMostPopular();

        IEnumerable<string> GetMostPopularByNames();

        IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairs();

        IEnumerable<KeyValuePair<string, string>> GetMostPopularByKeyValuePairs();
    }
}
