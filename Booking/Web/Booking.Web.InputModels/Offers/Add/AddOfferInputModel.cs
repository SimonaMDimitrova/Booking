namespace Booking.Web.InputModels.Offers.Add
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class AddOfferInputModel : OfferBaseInputModel
    {
        public IEnumerable<int> OfferFacilitiesIds { get; set; }

        public IEnumerable<AddOfferFacilityInputModel> OfferFacilities { get; set; }

        public IEnumerable<KeyValuePair<int, string>> BedTypes { get; set; }

        public IEnumerable<int> BedTypesCounts { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
