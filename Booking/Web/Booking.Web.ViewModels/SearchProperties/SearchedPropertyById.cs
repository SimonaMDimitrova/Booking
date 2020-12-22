namespace Booking.Web.ViewModels.SearchProperties
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.PropertiesViewModels;

    public class SearchedPropertyById : PropertyByIdBaseViewModel
    {
        public IEnumerable<SearchedOfferViewModel> Offers { get; set; }
    }
}
