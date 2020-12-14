namespace Booking.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Booking.Data.Common.Models;

    public class PropertyImage : BaseModel<string>
    {
        public PropertyImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [Required]
        public string Extension { get; set; }
    }
}
