namespace Booking.Data.Seeding.Datasets
{
    using System.ComponentModel.DataAnnotations;

    using Booking.Common;
    using Newtonsoft.Json;

    internal class CountryModel
    {
        [JsonRequired]
        [MinLength(GlobalConstants.CountryNameMinLength)]
        [MaxLength(GlobalConstants.CountryNameMaxLength)]
        [JsonProperty("country")]
        public string Country { get; set; }

        [Required]
        [MaxLength(GlobalConstants.CurrencyCodeMaxLength)]
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
    }
}
