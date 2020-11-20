namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(Currency))]
        public int CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}
