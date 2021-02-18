namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Services.Mapping;
    using Booking.Web.InputModels.PropertiesInputModels.Add;
    using Booking.Web.InputModels.PropertiesInputModels.Edit;

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

        public IEnumerable<T> GetAllInGeneralCategory<T>()
        {
            return this.facilitiesRepository
                .All()
                .Where(f => f.FacilityCategory.Name == "General")
                .To<T>()
                .ToList();
        }

        public IEnumerable<EditPropertyFacilityInputModel> GetAllByPropertyId(string id)
        {
            var facilities = new List<EditPropertyFacilityInputModel>();
            var generalFacilities = this.GetAllInGeneralCategory<AddFacilityIdNameInputModel>();
            var propertyFacilitiesDb = this.propertyFaciltiesRepository.All();
            foreach (var facility in generalFacilities)
            {
                var isChecked = propertyFacilitiesDb
                    .Any(p => p.PropertyId == id && p.Facility.Name == facility.Name);
                var facilityInputModel = new EditPropertyFacilityInputModel
                {
                    Name = facility.Name,
                    Id = facility.Id,
                    IsChecked = isChecked,
                };

                facilities.Add(facilityInputModel);
            }

            return facilities
                .OrderBy(f => f.Name);
        }

        public IEnumerable<T> GetAllExeptInGeneralCategory<T>()
        {
            return this.facilitiesRepository
                .All()
                .Where(f => f.FacilityCategory.Name != "General")
                .OrderBy(f => f.Name)
                .To<T>()
                .ToList();
        }
    }
}
