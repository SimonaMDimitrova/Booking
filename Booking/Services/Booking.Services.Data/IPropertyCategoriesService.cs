namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface IPropertyCategoriesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairs();
    }
}
