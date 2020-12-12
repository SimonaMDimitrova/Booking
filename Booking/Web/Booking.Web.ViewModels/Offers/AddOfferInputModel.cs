namespace Booking.Web.ViewModels.Offers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Web.Infrastructure.ValidationAttributes;
    using Booking.Web.ViewModels.OffersFacilities;

    public class AddOfferInputModel
    {
        public string PropertyId { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price is required. It must be more than 1.00.")]
        [Display(Name = "Price per person - ")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Valid from field is required.")]
        [DateMinValueAttribute]
        [Display(Name = "Valid from")]
        public DateTime? ValidFrom { get; set; }

        [Required(ErrorMessage = "Valid to field is required.")]
        [Display(Name = "Valid to")]
        public DateTime? ValidTo { get; set; }

        public IEnumerable<int> OfferFacilitiesIds { get; set; }

        public IEnumerable<OfferFacilityInputModel> OfferFacilities { get; set; }

        public IEnumerable<KeyValuePair<int, string>> BedTypes { get; set; }

        public IEnumerable<int> BedTypesCounts { get; set; }
    }
}
