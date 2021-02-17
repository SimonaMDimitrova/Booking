namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.InputModels.Offers.Add;
    using Booking.Web.InputModels.PropertiesInputModels.Add;
    using Booking.Web.InputModels.PropertiesInputModels.Edit;

    public interface IFacilitiesService
    {
        IEnumerable<AddFacilityIdNameInputModel> GetAllInGeneralCategory();

        IEnumerable<OfferFacilityInputModel> GetAllExeptInGeneralCategory();

        IEnumerable<EditPropertyFacilityInputModel> GetAllByPropertyId(string id);
    }
}
