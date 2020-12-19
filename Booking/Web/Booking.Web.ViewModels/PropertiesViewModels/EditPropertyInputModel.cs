namespace Booking.Web.ViewModels.PropertiesViewModels
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.Rules;

    public class EditPropertyInputModel : PropertyBaseInputModel
    {
        public string Id { get; set; }

        public IEnumerable<EditRuleInputModel> Rules { get; set; }

        public IEnumerable<PropertyFacilityInputModel> Facilities { get; set; }

        public IEnumerable<string> AllImages { get; set; }

        public IEnumerable<string> ImagesToBeRemoved { get; set; }
    }
}
