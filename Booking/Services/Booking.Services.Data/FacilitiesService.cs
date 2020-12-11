namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.PropertyFacilities;

    public class FacilitiesService : IFacilitiesService
    {
        private readonly IRepository<Facility> facilitiesRepository;
        private readonly IRepository<PropertyFacility> propertyFaciltiesRepository;

        public FacilitiesService(
            IRepository<Facility> facilitiesRepository,
            IRepository<PropertyFacility> propertyFaciltiesRepository)
        {
            this.facilitiesRepository = facilitiesRepository;
            this.propertyFaciltiesRepository = propertyFaciltiesRepository;
        }

        public IEnumerable<FacilityIdNameViewModel> GetAllFacilities()
        {
            return this.facilitiesRepository
                .AllAsNoTracking()
                .Where(f => f.FacilityCategory.Name == "General")
                .Select(f => new FacilityIdNameViewModel
                {
                    Name = f.Name,
                    Id = f.Id,
                })
                .ToList();
        }

        public IEnumerable<PropertyFacilityViewModel> GetAllFacilitiesByPropertyId(string id)
        {
            var facilitiesInListViewModel = new List<PropertyFacilityViewModel>();
            var facilities = this.GetAllFacilities();
            foreach (var facility in facilities)
            {
                var isChecked = this.propertyFaciltiesRepository
                    .AllAsNoTracking()
                    .Any(p => p.PropertyId == id && p.Facility.Name == facility.Name);
                var facilityViewModel = new PropertyFacilityViewModel
                {
                    Name = facility.Name,
                    Id = facility.Id,
                    IsChecked = isChecked,
                };

                facilitiesInListViewModel.Add(facilityViewModel);
            }

            return facilitiesInListViewModel;
        }
    }
}
