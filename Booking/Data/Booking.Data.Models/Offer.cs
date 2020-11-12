namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Booking.Data.Common.Models;

    public class Offer : BaseDeletableModel<string>
    {
        public Offer()
        {
            this.Id = Guid.NewGuid().ToString();

            this.Facilities = new HashSet<Facility>();
            this.Rooms = new HashSet<Room>();
        }

        public decimal Price { get; set; }

        public bool IsCreditCardAllowed { get; set; }

        public byte CancellationDays { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        [ForeignKey(nameof(ApplicationUser))]
        [Required]
        public string ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [ForeignKey(nameof(Offer))]
        public string ReservationId { get; set; }

        public Reservation Reservation { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        // TODO: Photos entity
    }
}
