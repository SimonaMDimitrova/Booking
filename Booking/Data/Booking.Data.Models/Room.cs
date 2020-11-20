namespace Booking.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Data.Common.Models;

    public class Room : BaseDeletableModel<int>
    {
        public Room()
        {
            this.RoomBedTypes = new HashSet<RoomBedType>();
        }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public virtual ICollection<RoomBedType> RoomBedTypes { get; set; }
    }
}
