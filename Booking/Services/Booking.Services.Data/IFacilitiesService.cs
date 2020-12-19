namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.Facilities;

    public interface IFacilitiesService
    {
        IEnumerable<FacilityIdNameInputModel> GetAllInGeneralCategory();

        IEnumerable<OfferFacilityInputModel> GetAllExeptInGeneralCategory();

        IEnumerable<PropertyFacilityInputModel> GetAllByPropertyId(string id);
    }
}
