namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Data.Common.Models;

    public class BedType : BaseModel<int>
    {
        public BedType()
        {
            this.OfferBedTypes = new HashSet<OfferBedType>();
        }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public byte Capacity { get; set; }

        public virtual ICollection<OfferBedType> OfferBedTypes { get; set; }
    }
}
