namespace Booking.Services.Data
{
    using System.Collections.Generic;

    public interface ITownsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairBasedOnCountryId(int id);

        IEnumerable<KeyValuePair<string, string>> GetMostPopularByKeyValuePairBasedOnCountryId(int id);
    }
}
