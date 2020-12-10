namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.PropertyFacilities;

    public interface IFacilitiesService
    {
        IEnumerable<FacilityIdNameViewModel> GetAllFacilities();

        IEnumerable<PropertyFacilityViewModel> GetAllFacilitiesByPropertyId(string id);
    }
}
