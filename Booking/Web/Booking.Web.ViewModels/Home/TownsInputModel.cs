namespace Booking.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class TownsInputModel
    {
        public IEnumerable<KeyValuePair<string, string>> Towns { get; set; }

        public string TownId { get; set; }
    }
}
