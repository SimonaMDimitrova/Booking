namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.PropertiesViewModels.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.Edit;

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

        public IEnumerable<FacilityIdNameInputModel> GetAllInGeneralCategory()
        {
            return this.facilitiesRepository
                .AllAsNoTracking()
                .Where(f => f.FacilityCategory.Name == "General")
                .Select(f => new FacilityIdNameInputModel
                {
                    Name = f.Name,
                    Id = f.Id,
                })
                .ToList();
        }

        public IEnumerable<PropertyFacilityInputModel> GetAllByPropertyId(string id)
        {
            var facilitiesInListViewModel = new List<PropertyFacilityInputModel>();
            var facilities = this.GetAllInGeneralCategory();
            foreach (var facility in facilities)
            {
                var isChecked = this.propertyFaciltiesRepository
                    .All()
                    .Any(p => p.PropertyId == id && p.Facility.Name == facility.Name);
                var facilityViewModel = new PropertyFacilityInputModel
                {
                    Name = facility.Name,
                    Id = facility.Id,
                    IsChecked = isChecked,
                };

                facilitiesInListViewModel.Add(facilityViewModel);
            }

            return facilitiesInListViewModel;
        }

        public IEnumerable<OfferFacilityInputModel> GetAllExeptInGeneralCategory()
        {
            return this.facilitiesRepository
                .All()
                .Where(f => f.FacilityCategory.Name != "General")
                .Select(f => new OfferFacilityInputModel
                {
                    Name = f.Name,
                    Id = f.Id,
                    Category = f.FacilityCategory.Name,
                })
                .OrderBy(f => f.Name)
                .ToList();
        }
    }
}
