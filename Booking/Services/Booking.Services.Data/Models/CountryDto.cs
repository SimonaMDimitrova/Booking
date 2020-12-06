namespace Booking.Services.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CountryDto
    {
        public IEnumerable<TownDto> Towns { get; set; }

        public string CurrencyCode { get; set; }
    }
}
