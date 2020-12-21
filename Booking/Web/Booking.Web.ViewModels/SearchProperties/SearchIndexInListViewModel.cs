namespace Booking.Web.ViewModels.SearchProperties
{
    using System;

    using Booking.Web.ViewModels.PropertiesViewModels;

    public class SearchIndexInListViewModel : PropertyByIdViewModel
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }
    }
}
