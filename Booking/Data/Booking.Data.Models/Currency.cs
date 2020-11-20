namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Data.Common.Models;

    public class Currency : BaseDeletableModel<int>
    {
        public Currency()
        {
            this.Countries = new HashSet<Country>();
        }

        [Required]
        [MaxLength(3)]
        public string CurrencyCode { get; set; }

        public virtual ICollection<Country> Countries { get; set; }
    }
}
