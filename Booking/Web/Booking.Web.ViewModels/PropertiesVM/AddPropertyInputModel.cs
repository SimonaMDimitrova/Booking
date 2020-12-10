namespace Booking.Web.ViewModels.PropertiesVM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.Rules;

    public class AddPropertyInputModel : PropertyBaseInputModel
    {
        public IEnumerable<KeyValuePair<string, string>> PropertyCategories { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Countries { get; set; }

        public IEnumerable<RuleIdNameViewModel> Rules { get; set; }

        public IEnumerable<FacilityIdNameViewModel> Facilities { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a country.")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a town.")]
        [Display(Name = "Town")]
        public int TownId { get; set; }

        [Required(ErrorMessage = "Enter address.")]
        [MinLength(5, ErrorMessage = "Address must be between 5 and 200 characters long.")]
        [MaxLength(200, ErrorMessage = "Address must be between 5 and 200 characters long.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Choose property category.")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose property category.")]
        [Display(Name = "Property category")]
        public int PropertyCategoryId { get; set; }
    }
}
