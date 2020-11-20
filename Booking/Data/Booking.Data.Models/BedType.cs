namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Data.Common.Models;

    public class BedType : BaseDeletableModel<int>
    {
        public BedType()
        {
            this.RoomBedTypes = new HashSet<RoomBedType>();
        }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        public byte Capacity { get; set; }

        public virtual ICollection<RoomBedType> RoomBedTypes { get; set; }
    }
}
