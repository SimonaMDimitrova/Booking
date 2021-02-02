namespace Booking.Web.InputModels.PropertiesInputModels.Add
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Common;

    public class AddPropertyInputModel : PropertyBaseInputModel
    {
        public IEnumerable<KeyValuePair<string, string>> PropertyCategories { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Countries { get; set; }

        public IEnumerable<RuleInputModel> Rules { get; set; }

        public IEnumerable<FacilityIdNameInputModel> Facilities { get; set; }

        [Required]
        [Range(GlobalConstants.MinCountryId, int.MaxValue, ErrorMessage = GlobalConstants.ErrorMessages.Country)]
        [Display(Name = GlobalConstants.CountryDisplayName)]
        public int CountryId { get; set; }

        [Required]
        [Range(GlobalConstants.MinTownId, int.MaxValue, ErrorMessage = GlobalConstants.ErrorMessages.Town)]
        [Display(Name = GlobalConstants.TownDisplayName)]
        public int TownId { get; set; }

        [Required]
        [MinLength(GlobalConstants.PropertyAddressMinLength, ErrorMessage = GlobalConstants.ErrorMessages.PropertyAddress)]
        [MaxLength(GlobalConstants.PropertyAddressMaxLength, ErrorMessage = GlobalConstants.ErrorMessages.PropertyAddress)]
        public string Address { get; set; }

        [Required]
        [Range(GlobalConstants.PropertyCategoryMinId, int.MaxValue, ErrorMessage = GlobalConstants.ErrorMessages.PropertyCategories)]
        [Display(Name = GlobalConstants.PropertyCategoryDisplayName)]
        public int PropertyCategoryId { get; set; }
    }
}
