namespace Booking.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Booking.Data.Common.Models;

    public class OfferImage : BaseModel<string>
    {
        public OfferImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string OfferId { get; set; }

        public virtual Offer Offer { get; set; }

        [Required]
        public string Extension { get; set; }
    }
}
