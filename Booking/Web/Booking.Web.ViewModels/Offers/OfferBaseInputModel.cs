namespace Booking.Web.ViewModels.Offers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Booking.Web.Infrastructure.ValidationAttributes;

    public abstract class OfferBaseInputModel
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price is required. It must be more than 1.00.")]
        public decimal PricePerPerson { get; set; }

        [Required(ErrorMessage = "Valid from field is required.")]
        [DateMinValueAttribute]
        [Display(Name = "Valid from")]
        public DateTime? ValidFrom { get; set; }

        [Required(ErrorMessage = "Valid to field is required.")]
        [Display(Name = "Valid to")]
        public DateTime? ValidTo { get; set; }

        public string CurrencyCode { get; set; }
    }
}
