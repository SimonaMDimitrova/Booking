namespace Booking.Web.ViewModels.PropertiesVM
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.Offers;

    public class PropertyByIdViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string PropertyType { get; set; }

        public string Description { get; set; }

        public byte Stars { get; set; }

        public byte Floors { get; set; }

        public IEnumerable<string> Facilities { get; set; }

        public string Country { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

        public string PropertyCategory { get; set; }

        public ICollection<OfferViewModel> Offers { get; set; }

        public string CurrencyCode { get; set; }
    }
}
