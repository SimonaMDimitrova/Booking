namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.Facilities;

    public interface IFacilitiesService
    {
        IEnumerable<PropertyFacilityIdNameViewModel> GetPropertyFacilities();
    }
}
