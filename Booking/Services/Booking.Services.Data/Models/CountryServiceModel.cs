namespace Booking.Services.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CountryServiceModel
    {
        public IEnumerable<TownServiceModel> Towns { get; set; }

        public string CurrencyCode { get; set; }
    }
}
