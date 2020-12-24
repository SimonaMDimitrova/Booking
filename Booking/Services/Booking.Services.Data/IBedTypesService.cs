namespace Booking.Services.Data
{
    using System.Collections.Generic;

    public interface IBedTypesService
    {
        IEnumerable<KeyValuePair<int, string>> GetAll();
    }
}
