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
        private readonly IDeletableEntityRepository<Offer> offersRepository;
        private readonly IDeletableEntityRepository<PropertyFacility> propertyFacilitiesRepository;

        public PropertiesService(
            IDeletableEntityRepository<Property> propertiesRepository,
            IRepository<Rule> rulesRepository,
            IDeletableEntityRepository<Offer> offersRepository,
            IDeletableEntityRepository<PropertyFacility> propertyFacilitiesRepository)
        {
            this.propertiesRepository = propertiesRepository;
            this.rulesRepository = rulesRepository;
            this.offersRepository = offersRepository;
            this.propertyFacilitiesRepository = propertyFacilitiesRepository;
        }

        public bool CheckIfNewPropertyNameAvailable(string name, string propertyId)
        {
            return this.propertiesRepository
                .All()
                .Any(p => p.Name == name && p.Id != propertyId);
        }

        public bool CheckIsPropertyNameAvailable(string name)
        {
            return this.propertiesRepository
                .All()
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

            var facilitiesIds = input.FacilitiesIds;
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

        public async Task DeleteAsync(string propertyId)
        {
            var property = this.propertiesRepository
                .AllAsNoTracking()
                .FirstOrDefault(p => p.Id == propertyId);
            var offers = property.Offers;

            if (offers != null)
            {
                foreach (var offer in offers)
                {
                    this.offersRepository.Delete(offer);
                }
            }

            this.propertiesRepository.Delete(property);
            await this.propertiesRepository.SaveChangesAsync();
        }

        public async Task EditProperty(EditPropertyInputModel input)
        {
            var property = this.propertiesRepository
                .All()
                .FirstOrDefault(p => input.Id == p.Id);
            property.Name = input.Name;
            property.Floors = input.Floors;
            property.Stars = input.Stars;
            property.Description = input.Description;

            var propertyRules = this.propertiesRepository
                .AllWithDeleted()
                .Where(p => p.Id == input.Id)
                .Select(p => p.PropertyRules)
                .FirstOrDefault();
            var rulesIds = input.RulesIds;
            if (rulesIds != null)
            {
                foreach (var propertyRule in propertyRules)
                {
                    propertyRule.IsAllowed = rulesIds.Contains(propertyRule.RuleId) ? true : false;
                }
            }
            else
            {
                foreach (var propertyRule in propertyRules)
                {
                    propertyRule.IsAllowed = false;
                }
            }

            var propertyFacilities = this.propertiesRepository
                .All()
                .Where(p => p.Id == input.Id)
                .Select(p => p.PropertyFacilities)
                .FirstOrDefault();
            var facilitiesIds = input.FacilitiesIds;
            if (facilitiesIds != null)
            {
                if (propertyFacilities != null)
                {
                    foreach (var facilityId in facilitiesIds)
                    {
                        var isFacilityAlreadyAdded = propertyFacilities.Any(f => f.FacilityId == facilityId);
                        var isFacilityAlreadyAddedDeleted = propertyFacilities.Any(f => f.FacilityId == facilityId && f.IsDeleted == true);
                        if (isFacilityAlreadyAdded == false)
                        {
                            var propertyFacility = new PropertyFacility
                            {
                                Property = property,
                                FacilityId = facilityId,
                            };

                            await this.propertyFacilitiesRepository.AddAsync(propertyFacility);
                        }

                        if (isFacilityAlreadyAddedDeleted == true)
                        {
                            var propertyFacility = this.propertyFacilitiesRepository.AllWithDeleted().FirstOrDefault(f => f.FacilityId == facilityId);
                            this.propertyFacilitiesRepository.Undelete(propertyFacility);
                        }
                    }
                }
                else
                {
                    foreach (var facilityId in facilitiesIds)
                    {
                        var propertyFacility = new PropertyFacility
                        {
                            Property = property,
                            FacilityId = facilityId,
                        };
                        await this.propertyFacilitiesRepository.AddAsync(propertyFacility);
                    }
                }
            }
            else
            {
                if (propertyFacilities != null)
                {
                    foreach (var propertyFacility in propertyFacilities)
                    {
                        this.propertyFacilitiesRepository.Delete(propertyFacility);
                    }
                }
            }

            await this.propertyFacilitiesRepository.SaveChangesAsync();
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
                        Id = p.Id,
                    }),
            };

            return properties;
        }

        public EditPropertyInputModel GetPropertyById(string propertyId, string userId)
        {
            var property = this.propertiesRepository
                .AllAsNoTracking()
                .Where(p => p.Id == propertyId && p.ApplicationUserId == userId)
                .Select(p => new EditPropertyInputModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    Stars = p.Stars,
                    Floors = p.Floors,
                    Id = p.Id,
                })
                .FirstOrDefault();

            return property;
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
