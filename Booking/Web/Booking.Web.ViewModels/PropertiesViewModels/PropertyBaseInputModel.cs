namespace Booking.Web.ViewModels.PropertiesViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Common;
    using Microsoft.AspNetCore.Http;

    public abstract class PropertyBaseInputModel
    {
        [Required]
        [MinLength(GlobalConstants.PropertyNameMinLength, ErrorMessage = GlobalConstants.ErrorMessages.PropertyName)]
        [MaxLength(GlobalConstants.PropertyNameMaxLength, ErrorMessage = GlobalConstants.ErrorMessages.PropertyName)]
        public string Name { get; set; }

        [MaxLength(GlobalConstants.PropertyDescriptionMaxLength, ErrorMessage = GlobalConstants.ErrorMessages.PropertyDescription)]
        public string Description { get; set; }

        [Display(Name = GlobalConstants.PropertyRatingDisplay)]
        public byte PropertyRating { get; set; }

        [Required]
        [Range(GlobalConstants.PropertyFloorMin, GlobalConstants.PropertyFloorMax, ErrorMessage = GlobalConstants.ErrorMessages.PropertyFloors)]
        public byte Floors { get; set; }

        public IEnumerable<int> FacilitiesIds { get; set; }

        public IEnumerable<int> RulesIds { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}
