namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.PropertiesVM;

    public class PropertiesService : IPropertiesService
    {
        private readonly IDeletableEntityRepository<Property> propertiesRepository;
        private readonly IRepository<Rule> rulesRepository;

        public PropertiesService(
            IDeletableEntityRepository<Property> propertiesRepository,
            IRepository<Rule> rulesRepository)
        {
            this.propertiesRepository = propertiesRepository;
            this.rulesRepository = rulesRepository;
        }

        public bool CheckIsPropertyNameAvailable(string name)
        {
            return this.propertiesRepository
                .AllAsNoTracking()
                .Any(p => p.Name == name);
        }

        public async Task CreateAsync(AddPropertyInputModel input, string userId)
        {
            var property = new Property
            {
                Name = input.Name,
                Address = input.Address,
                PropertyCategoryId = input.PropertyCategoryId,
                Floors = input.Floors,
                Stars = input.Stars,
                TownId = input.TownId,
                ApplicationUserId = userId,
                Description = input.Description,
            };

            var rules = this.rulesRepository.AllAsNoTracking();
            var rulesIds = input.RulesIds;
            if (rulesIds != null)
            {
                foreach (var rule in rules)
                {
                    var propertyRule = new PropertyRule
                    {
                        Property = property,
                        RuleId = rule.Id,
                        IsAllowed = rulesIds.Contains(rule.Id),
                    };

                    property.PropertyRules.Add(propertyRule);
                }
            }
            else
            {
                foreach (var rule in rules)
                {
                    var propertyRule = new PropertyRule
                    {
                        Property = property,
                        RuleId = rule.Id,
                        IsAllowed = false,
                    };

                    property.PropertyRules.Add(propertyRule);
                }
            }

            var facilitiesIds = input.PropertyFacilitiesIds;
            if (facilitiesIds != null)
            {
                foreach (var facilityId in facilitiesIds)
                {
                    var propertyFacility = new PropertyFacility
                    {
                        Property = property,
                        FacilityId = facilityId,
                    };

                    property.PropertyFacilities.Add(propertyFacility);
                }
            }

            await this.propertiesRepository.AddAsync(property);
            await this.propertiesRepository.SaveChangesAsync();
        }

        public PropertyInListModel GetAllPropertiesByUserId(string userId)
        {
            var properties = new PropertyInListModel
            {
                Properties = this.propertiesRepository
                    .AllAsNoTracking()
                    .Where(p => p.ApplicationUserId == userId)
                    .Select(p => new PropertyViewModel
                    {
                        Name = p.Name,
                        Stars = p.Stars,
                        Country = p.Town.Country.Name,
                        Town = p.Town.Name,
                        PropertyCategory = p.PropertyCategory.Name,
                    }),
            };

            return properties;
        }

        public string GetPropertyIdByName(string propertyName)
        {
            return this.propertiesRepository
                .AllAsNoTracking()
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                })
                .FirstOrDefault(p => p.Name == propertyName)
                .Id;
        }
    }
}
