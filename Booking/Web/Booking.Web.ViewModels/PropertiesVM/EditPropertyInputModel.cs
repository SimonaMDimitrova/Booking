﻿namespace Booking.Web.ViewModels.PropertiesVM
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.PropertyFacilities;
    using Booking.Web.ViewModels.PropertyRules;

    public class EditPropertyInputModel : PropertyBaseInputModel
    {
        public string Id { get; set; }

        public IEnumerable<PropertyRuleViewModel> Rules { get; set; }

        public IEnumerable<PropertyFacilityViewModel> Facilities { get; set; }
    }
}
