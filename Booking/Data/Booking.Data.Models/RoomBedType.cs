namespace Booking.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class RoomBedType : BaseDeletableModel<string>
    {
        public RoomBedType()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [ForeignKey(nameof(Room))]
        [Required]
        public string RoomId { get; set; }

        public virtual Room Room { get; set; }

        [ForeignKey(nameof(BedType))]
        [Required]
        public string BedTypeId { get; set; }

        public virtual BedType BedType { get; set; }
    }
}
