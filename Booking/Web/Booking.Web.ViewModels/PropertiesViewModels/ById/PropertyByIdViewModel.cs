namespace Booking.Web.ViewModels.PropertiesViewModels.ById
{
    using System.Collections.Generic;

    public class PropertyByIdViewModel : PropertyByIdBaseViewModel
    {
        public IEnumerable<OfferViewModel> Offers { get; set; }
    }
}
