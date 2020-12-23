namespace Booking.Web.ViewModels.SearchProperties
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.PropertiesViewModels;

    public class SearchedPropertyByIdViewModel : PropertyByIdBaseViewModel
    {
        public IEnumerable<SearchedOfferViewModel> Offers { get; set; }
    }
}
