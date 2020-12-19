namespace Booking.Web.ViewModels.Offers
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.BedTypes;
    using Booking.Web.ViewModels.Facilities;

    public class OfferViewModel
    {
        public string Id { get; set; }

        public decimal Price { get; set; }

        public string ValidFrom { get; set; }

        public string ValidTo { get; set; }

        public IEnumerable<OfferFacilityViewModel> OfferFacilities { get; set; }

        public IEnumerable<BedTypeViewModel> Rooms { get; set; }

        public byte Guests { get; set; }

        public IEnumerable<string> Images { get; set; }
    }
}
