namespace Booking.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Booking.Data.Common.Models;

    public class RoomBedType : BaseDeletableModel<int>
    {
        [Required]
        [ForeignKey(nameof(Room))]
        public int RoomId { get; set; }

        public virtual Room Room { get; set; }

        [Required]
        [ForeignKey(nameof(BedType))]
        public int BedTypeId { get; set; }

        public virtual BedType BedType { get; set; }
    }
}
