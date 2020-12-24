namespace Booking.Services
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.BedTypes;
    using Booking.Web.ViewModels.Facilities;

    public interface IDictionariesService
    {
        IDictionary<string, int> CreateBedType(IEnumerable<BedTypeViewModel> bedTypes);

        IDictionary<string, List<string>> CreateFacilities(IEnumerable<OfferFacilityViewModel> facilities);
    }
}
