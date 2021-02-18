namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.InputModels.PropertiesInputModels.Edit;

    public interface IFacilitiesService
    {
        IEnumerable<T> GetAllInGeneralCategory<T>();

        IEnumerable<T> GetAllExeptInGeneralCategory<T>();

        IEnumerable<EditPropertyFacilityInputModel> GetAllByPropertyId(string id);
    }
}
