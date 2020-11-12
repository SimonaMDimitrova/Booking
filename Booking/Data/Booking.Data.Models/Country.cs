namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class Country : BaseDeletableModel<string>
    {
        public Country()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Towns = new HashSet<Town>();
        }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }

        [ForeignKey(nameof(Currency))]
        [Required]
        public string CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }
    }
}
