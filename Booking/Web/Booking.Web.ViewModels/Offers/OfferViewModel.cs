namespace Booking.Web.ViewModels.Offers
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.OffersFacilities;

    public class OfferViewModel
    {
        public decimal Price { get; set; }

        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }

        public ICollection<OfferFacilityViewModel> OfferFacilities { get; set; }

        public ICollection<string> Rooms { get; set; }

        public byte Guests { get; set; }
    }
}
