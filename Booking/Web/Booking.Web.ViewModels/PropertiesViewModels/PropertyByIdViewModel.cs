namespace Booking.Web.ViewModels.PropertiesViewModels
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.Offers;

    public class PropertyByIdViewModel : PropertyByIdBaseViewModel
    {
        public IEnumerable<OfferViewModel> Offers { get; set; }
    }
}
