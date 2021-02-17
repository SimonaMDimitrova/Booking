namespace Booking.Services.Data
{
    using System.Collections.Generic;

    public interface IRulesService
    {
        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllByPropertyId<T>(string id);
    }
}
