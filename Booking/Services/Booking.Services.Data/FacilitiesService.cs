namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Facilities;

    public class FacilitiesService : IFacilitiesService
    {
        private readonly IRepository<Facility> facilitiesRepository;

        public FacilitiesService(IRepository<Facility> facilitiesRepository)
        {
            this.facilitiesRepository = facilitiesRepository;
        }

        public IEnumerable<PropertyFacilityIdNameViewModel> GetPropertyFacilities()
        {
            return this.facilitiesRepository
                .AllAsNoTracking()
                .Where(f => f.FacilityCategory.Name == "General")
                .Select(f => new PropertyFacilityIdNameViewModel
                {
                    Name = f.Name,
                    Id = f.Id,
                })
                .ToList();
        }
    }
}
