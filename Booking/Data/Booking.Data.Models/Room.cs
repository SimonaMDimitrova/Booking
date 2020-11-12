namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Booking.Data.Common.Models;

    public class Room : BaseDeletableModel<string>
    {
        public Room()
        {
            this.Id = Guid.NewGuid().ToString();

            this.RoomBedTypes = new HashSet<RoomBedType>();
        }

        public virtual ICollection<RoomBedType> RoomBedTypes { get; set; }
    }
}
