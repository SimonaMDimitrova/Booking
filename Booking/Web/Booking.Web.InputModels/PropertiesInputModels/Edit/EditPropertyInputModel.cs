namespace Booking.Web.InputModels.PropertiesInputModels.Edit
{
    using System.Collections.Generic;

    public class EditPropertyInputModel : PropertyBaseInputModel
    {
        public string Id { get; set; }

        public IEnumerable<EditRuleInputModel> Rules { get; set; }

        public IEnumerable<PropertyFacilityInputModel> Facilities { get; set; }
    }
}
