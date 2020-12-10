namespace Booking.Web.ViewModels.PropertiesVM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.Rules;

    public class AddPropertyInputModel
    {
        public IEnumerable<KeyValuePair<string, string>> PropertyCategories { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Countries { get; set; }

        public IEnumerable<PropertyFacilityIdNameViewModel> PropertyFacilities { get; set; }

        public IEnumerable<RuleIdNameViewModel> Rules { get; set; }

        [Required(ErrorMessage = "Enter name.")]
        [MinLength(3, ErrorMessage = "Name must be between 3 and 150 characters long.")]
        [MaxLength(150, ErrorMessage = "Name must be between 3 and 150 characters long.")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a country.")]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a town.")]
        [Display(Name = "Town")]
        public int TownId { get; set; }

        [MaxLength(500, ErrorMessage = "Description can't be more than 500 characters long.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter address.")]
        [MinLength(5, ErrorMessage = "Address must be between 5 and 200 characters long.")]
        [MaxLength(200, ErrorMessage = "Address must be between 5 and 200 characters long.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Choose property rating (0-5 stars).")]
        [Range(0, 5, ErrorMessage = "Choose property rating (0-5 stars).")]
        [Display(Name = "Property rating")]
        public byte Stars { get; set; }

        [Required(ErrorMessage = "Choose property category.")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose property category.")]
        [Display(Name = "Property category")]
        public int PropertyCategoryId { get; set; }

        [Required(ErrorMessage = "Enter floors count (1-80).")]
        [Range(1, 80, ErrorMessage = "Enter floors count (1-80).")]
        public byte Floors { get; set; }

        public IEnumerable<int> PropertyFacilitiesIds { get; set; }

        public IEnumerable<int> RulesIds { get; set; }
    }
}
