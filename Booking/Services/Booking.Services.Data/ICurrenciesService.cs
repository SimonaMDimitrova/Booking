namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ICurrenciesService
    {
        string GetCurrencyCodeByCountryId(int id);

        string GetCurrencyByPropertyId(string id);
    }
}
