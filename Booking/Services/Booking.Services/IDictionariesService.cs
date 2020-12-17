namespace Booking.Services
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.BedTypes;
    using Booking.Web.ViewModels.OffersFacilities;

    public interface IDictionariesService
    {
        IDictionary<string, int> CreateBedTypeDictionary(IEnumerable<BedTypeViewModel> bedTypes);

        IDictionary<string, List<string>> CreateFacilitiesDictionary(IEnumerable<OfferFacilityViewModel> facilities);
    }
}
