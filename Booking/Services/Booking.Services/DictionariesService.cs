﻿namespace Booking.Services
{
    using System.Collections.Generic;

    using Booking.Services.Models;
    using Booking.Web.InputModels.Offers.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.ById;

    public class DictionariesService : IDictionariesService
    {
        public IDictionary<string, int> CreateBedTypes(IEnumerable<BedTypeViewModel> bedTypes)
        {
            var bedTypesDictionary = new Dictionary<string, int>();
            foreach (var bedType in bedTypes)
            {
                if (!bedTypesDictionary.ContainsKey(bedType.Type))
                {
                    bedTypesDictionary[bedType.Type] = 0;
                }

                bedTypesDictionary[bedType.Type] += bedType.Capacity;
            }

            return bedTypesDictionary;
        }

        public IDictionary<string, List<string>> CreateFacilities(IEnumerable<OfferFacilityViewModel> facilities)
        {
            var facilitiesDictionary = new Dictionary<string, List<string>>();
            foreach (var facility in facilities)
            {
                if (!facilitiesDictionary.ContainsKey(facility.Category))
                {
                    facilitiesDictionary[facility.Category] = new List<string>();
                }

                facilitiesDictionary[facility.Category].Add(facility.Name);
            }

            return facilitiesDictionary;
        }

        public IDictionary<string, ICollection<FacilityIdNameServiceModel>> CreateFacilitiesInput(IEnumerable<AddOfferFacilityInputModel> facilities)
        {
            var facilitiesDictionary = new SortedDictionary<string, ICollection<FacilityIdNameServiceModel>>();
            foreach (var facility in facilities)
            {
                if (!facilitiesDictionary.ContainsKey(facility.FacilityCategoryName))
                {
                    facilitiesDictionary[facility.FacilityCategoryName] = new List<FacilityIdNameServiceModel>();
                }

                facilitiesDictionary[facility.FacilityCategoryName]
                    .Add(new FacilityIdNameServiceModel { Name = facility.Name, Id = facility.Id });
            }

            return facilitiesDictionary;
        }
    }
}
