namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Data.Common.Models;

    public class BedType : BaseDeletableModel<string>
    {
        public BedType()
        {
            this.Id = Guid.NewGuid().ToString();

            this.RoomBedTypes = new HashSet<RoomBedType>();
        }

        [Required]
        [MaxLength(50)]
        public string Type { get; set; }

        [Required]
        public byte Capacity { get; set; }

        public virtual ICollection<RoomBedType> RoomBedTypes { get; set; }
    }
}
