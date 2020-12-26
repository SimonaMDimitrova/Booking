namespace Booking.Services
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.PropertiesViewModels.ById;

    public interface IDictionariesService
    {
        IDictionary<string, int> CreateBedTypes(IEnumerable<BedTypeViewModel> bedTypes);

        IDictionary<string, List<string>> CreateFacilities(IEnumerable<OfferFacilityViewModel> facilities);
    }
}
