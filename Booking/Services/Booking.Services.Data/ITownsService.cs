namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Services.Data.Models;

    public interface ITownsService
    {
        IEnumerable<KeyValuePair<string, string>> GetTownsByCountryId(int id);
    }
}
