namespace Booking.Data.Seeding.ImportDTOs
{
    using System.ComponentModel.DataAnnotations;

    using Newtonsoft.Json;

    internal class CountryCurrencyDto
    {
        [JsonRequired]
        [MinLength(2)]
        [MaxLength(100)]
        [JsonProperty("country")]
        public string Country { get; set; }

        [Required]
        [MaxLength(3)]
        [JsonProperty("currency_code")]
        public string CurrencyCode { get; set; }
    }
}
