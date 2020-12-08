namespace Booking.Web.ViewModels.Properties
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddPropertyInputModel
    {
        public IEnumerable<KeyValuePair<string, string>> PropertyCategories { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Countries { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Town")]
        public int TownId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [Range(0, 5)]
        public byte Stars { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [Display(Name = "Property category")]
        public int PropertyCategoryId { get; set; }

        [Required]
        [Range(1, byte.MaxValue)]
        public byte Floors { get; set; }
    }
}
