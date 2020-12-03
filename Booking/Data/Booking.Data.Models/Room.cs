namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using global::Booking.Data.Common.Models;

    public class Room : BaseDeletableModel<int>
    {
        public Room()
        {
            this.RoomBedTypes = new HashSet<RoomBedType>();
        }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        [Required]
        [ForeignKey(nameof(Offer))]
        public string OfferId { get; set; }

        public Offer Offer { get; set; }

        public virtual ICollection<RoomBedType> RoomBedTypes { get; set; }
    }
}
