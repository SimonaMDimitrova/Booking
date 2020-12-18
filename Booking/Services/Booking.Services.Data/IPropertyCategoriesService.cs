namespace Booking.Services.Data
{
    using System.Collections.Generic;

    public interface IPropertyCategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairs();
    }
}
