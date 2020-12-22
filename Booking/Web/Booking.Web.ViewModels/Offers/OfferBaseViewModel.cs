namespace Booking.Web.ViewModels.Offers
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.BedTypes;
    using Booking.Web.ViewModels.Facilities;

    public class OfferBaseViewModel
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public IEnumerable<OfferFacilityViewModel> OfferFacilities { get; set; }

        public IEnumerable<BedTypeViewModel> Rooms { get; set; }

        public byte Guests { get; set; }

        public IEnumerable<string> Images { get; set; }
    }
}
