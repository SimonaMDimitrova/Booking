namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IBedTypesService
    {
        IEnumerable<KeyValuePair<int, string>> GetAllBedTypes();
    }
}
