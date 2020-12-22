namespace Booking.Web.ViewModels.Offers
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.BedTypes;
    using Booking.Web.ViewModels.Facilities;

    public class OfferViewModel : OfferBaseViewModel
    {
        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }
    }
}
