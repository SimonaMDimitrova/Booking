namespace Booking.Services.Data
{
    using System.Collections.Generic;

    public interface ITownsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllByCountryId(int id);
    }
}
