namespace Booking.Web.ViewModels.PropertiesViewModels
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.PropertiesViewModels.ById;

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
