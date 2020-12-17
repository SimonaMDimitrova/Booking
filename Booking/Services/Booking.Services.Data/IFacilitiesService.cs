﻿namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.OffersFacilities;
    using Booking.Web.ViewModels.PropertyFacilities;

    public interface IFacilitiesService
    {
        IEnumerable<FacilityIdNameInputModel> GetAllGeneralFacilities();

        IEnumerable<OfferFacilityInputModel> GetAllFacilitiesExceptGeneral();

        IEnumerable<PropertyFacilityViewModel> GetAllFacilitiesByPropertyId(string id);
    }
}
