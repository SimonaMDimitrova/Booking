namespace Booking.Web.ViewModels.SearchProperties
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Booking.Web.ViewModels.PropertiesViewModels;

    public class SearchIndexViewModel : PropertyByIdViewModel
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }
    }
}
