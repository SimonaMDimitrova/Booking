namespace Booking.Web.ViewModels.Offers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Web.Infrastructure.ValidationAttributes;
    using Booking.Web.ViewModels.Facilities;
    using Microsoft.AspNetCore.Http;

    public class AddOfferInputModel : OfferBaseInputModel
    {
        public string PropertyId { get; set; }

        public IEnumerable<int> OfferFacilitiesIds { get; set; }

        public IEnumerable<OfferFacilityInputModel> OfferFacilities { get; set; }

        public IEnumerable<KeyValuePair<int, string>> BedTypes { get; set; }

        public IEnumerable<int> BedTypesCounts { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
