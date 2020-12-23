namespace Booking.Web.ViewModels.ViewComponents.SearchResults
{
    using System;
    using System.Collections.Generic;

    public class SearchIndexListViewModel
    {
        public IEnumerable<SearchIndexInListViewModel> Properties { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public byte Members { get; set; }
    }
}
