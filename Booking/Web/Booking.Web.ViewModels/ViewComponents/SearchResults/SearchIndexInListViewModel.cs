namespace Booking.Web.ViewModels.ViewComponents.SearchResults
{
    using System;

    using Booking.Web.ViewModels.PropertiesViewModels;

    public class SearchIndexInListViewModel : PropertyInListViewModel
    {
        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int OffersCount { get; set; }
    }
}
