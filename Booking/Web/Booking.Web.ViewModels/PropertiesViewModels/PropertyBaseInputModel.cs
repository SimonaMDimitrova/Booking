namespace Booking.Web.ViewModels.PropertiesViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public abstract class PropertyBaseInputModel
    {
        [Required(ErrorMessage = "Enter name.")]
        [MinLength(3, ErrorMessage = "Name must be between 3 and 150 characters long.")]
        [MaxLength(150, ErrorMessage = "Name must be between 3 and 150 characters long.")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "Description can't be more than 500 characters long.")]
        public string Description { get; set; }

        [Display(Name = "Property rating")]
        public byte PropertyRating { get; set; }

        public byte Floors { get; set; }

        public IEnumerable<int> FacilitiesIds { get; set; }

        public IEnumerable<int> RulesIds { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
