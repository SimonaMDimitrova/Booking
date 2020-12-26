namespace Booking.Services
{
    using System.Collections.Generic;

    using Booking.Services.Models;
    using Booking.Web.ViewModels.Offers.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.ById;

    public interface IDictionariesService
    {
        IDictionary<string, int> CreateBedTypes(IEnumerable<BedTypeViewModel> bedTypes);

        IDictionary<string, List<string>> CreateFacilities(IEnumerable<OfferFacilityViewModel> facilities);

        IDictionary<string, List<FacilityIdNameServiceModel>> CreateFacilitiesInput(IEnumerable<OfferFacilityInputModel> facilities);
    }
}
