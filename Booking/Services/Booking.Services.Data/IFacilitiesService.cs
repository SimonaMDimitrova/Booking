namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.PropertiesViewModels.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.Edit;

    public interface IFacilitiesService
    {
        IEnumerable<FacilityIdNameInputModel> GetAllInGeneralCategory();

        IEnumerable<OfferFacilityInputModel> GetAllExeptInGeneralCategory();

        IEnumerable<PropertyFacilityInputModel> GetAllByPropertyId(string id);
    }
}
