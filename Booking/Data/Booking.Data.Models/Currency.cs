namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Common;
    using global::Booking.Data.Common.Models;

    public class Currency : BaseModel<int>
    {
        public Currency()
        {
            this.Countries = new HashSet<Country>();
        }

        [Required]
        [MaxLength(GlobalConstants.CurrencyCodeMaxLength)]
        public string CurrencyCode { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
