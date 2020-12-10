namespace Booking.Web.ViewModels.PropertiesVM
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.Rules;

    public class PropertyBaseInputModel
    {
        [Required(ErrorMessage = "Enter name.")]
        [MinLength(3, ErrorMessage = "Name must be between 3 and 150 characters long.")]
        [MaxLength(150, ErrorMessage = "Name must be between 3 and 150 characters long.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description can't be more than 500 characters long.")]
        public string Description { get; set; }

        public byte Stars { get; set; }

        public byte Floors { get; set; }

        public IEnumerable<int> FacilitiesIds { get; set; }

        public IEnumerable<int> RulesIds { get; set; }
    }
}
