namespace Booking.Web.InputModels.Offers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Booking.Common;
    using Booking.Web.Infrastructure.ValidationAttributes;

    public class OfferBaseInputModel
    {
        public string PropertyId { get; set; }

        public string PropertyName { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = GlobalConstants.ErrorMessages.OfferPrice)]
        public decimal PricePerPerson { get; set; }

        [Required(ErrorMessage = GlobalConstants.ErrorMessages.OfferValidFromRequired)]
        [DateMinValueAttribute]
        [Display(Name = GlobalConstants.ValidFromDisplayName)]
        public DateTime? ValidFrom { get; set; }

        [Required(ErrorMessage = GlobalConstants.ErrorMessages.OfferValidToRequired)]
        [Display(Name = GlobalConstants.ValidToDisplayName)]
        public DateTime? ValidTo { get; set; }

        public string CurrencyCode { get; set; }

        [Required]
        [Range(GlobalConstants.MinOfferCount, GlobalConstants.MaxOfferCount, ErrorMessage = GlobalConstants.ErrorMessages.OffersCount)]
        public byte Count { get; set; }
    }
}
