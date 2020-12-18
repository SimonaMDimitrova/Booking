namespace Booking.Web.ViewModels.PropertiesViewModels
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.PropertyFacilities;
    using Booking.Web.ViewModels.PropertyRules;

    public class EditPropertyInputModel : PropertyBaseInputModel
    {
        public string Id { get; set; }

        public IEnumerable<PropertyRuleViewModel> Rules { get; set; }

        public IEnumerable<PropertyFacilityInputModel> Facilities { get; set; }

        public IEnumerable<string> AllImages { get; set; }

        public IEnumerable<string> ImagesToBeRemoved { get; set; }
    }
}
