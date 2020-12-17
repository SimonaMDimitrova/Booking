namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Home;
    using Booking.Web.ViewModels.Offers;
    using Booking.Web.ViewModels.OffersFacilities;
    using Booking.Web.ViewModels.PropertiesViewModels;
    using Booking.Web.ViewModels.SearchProperties;

    public class PropertiesService : IPropertiesService
    {
        private const string EditError = "Access denied. You don't have permission to edit another user's properties";

        private readonly string[] allowedExtensions = new[] { "jpg", "png" };
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

        public bool CheckIfEditInputNameIsAvailable(string name, string propertyId)
        {
            return this.propertiesRepository
                .All()
                .Any(p => p.Name == name && p.Id != propertyId);
        }

        public bool CheckIfNameIsAvailable(string name)
        {
            return this.propertiesRepository
                .All()
                .Any(p => p.Name == name);
        }

        public SearchIndexInListViewModel GetBySearchRequirements(SearchIndexInputModel input)
        {
            var isCountryIdValid = int.TryParse(input.CountryId, out int countryId);
            var townIdParsed = int.TryParse(input.TownId, out int townId);

            // TODO: check checkIn and Out
            if (input.CheckIn == null
                || input.CheckOut == null
                || !isCountryIdValid
                || !townIdParsed
                || countryId <= 0
                || townId <= 0
                || input.Members <= 0
                || input.MinBudget < 0
                || input.MinBudget >= input.MaxBudget)
            {
                return null;
            }

            var properties = this.propertiesRepository
                .All()
                .Where(
                    p => p.TownId == townId
                    && p.Town.CountryId == countryId)
                .Select(p => new SearchIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    Country = p.Town.Country.Name,
                    Town = p.Town.Name,
                    Description = p.Description,
                    Floors = p.Floors,
                    CheckIn = (DateTime)input.CheckIn,
                    CheckOut = (DateTime)input.CheckOut,
                    Stars = p.Stars,
                    PropertyCategory = p.PropertyCategory.Name,
                    PropertyType = p.PropertyCategory.PropertyType.Name,
                    CurrencyCode = p.Town.Country.Currency.CurrencyCode,
                    Facilities = p.PropertyFacilities
                        .Select(f => f.Facility.Name)
                        .ToList(),
                    Offers = p.Offers
                    .Where(
                        o => o.ValidFrom >= input.CheckIn
                            && o.ValidTo <= input.CheckOut)
                        .Select(o => new OfferViewModel
                        {
                            Id = o.Id,
                            Price = o.PricePerPerson,
                            ValidFrom = o.ValidFrom.ToString("dd/MM/yyyy"),
                            ValidTo = o.ValidTo.ToString("dd/MM/yyyy"),
                            OfferFacilities = o.OfferFacilities
                                .Select(f => new OfferFacilityViewModel
                                {
                                    Name = f.Facility.Name,
                                    Category = f.Facility.FacilityCategory.Name,
                                })
                                .ToList(),
                            Rooms = o.OfferBedTypes.Select(b => b.BedType.Type).ToList(),
                            Guests = (byte)o.OfferBedTypes.Sum(b => b.BedType.Capacity),
                        })
                        .Where(
                            o => o.Guests == input.Members
                            && (o.Price >= input.MinBudget || o.Price <= input.MaxBudget))
                        .ToList(),
                })
                .ToList();

            if (properties == null)
            {
                return null;
            }

            var propertiesViewModel = new SearchIndexInListViewModel();
            propertiesViewModel.Properties = properties;

            return propertiesViewModel;
        }

        public async Task CreateAsync(AddPropertyInputModel input, string userId, string imagePath)
        {
            var property = new Property
            {
                Name = input.Name,
                Address = input.Address,
                PropertyCategoryId = input.PropertyCategoryId,
                Floors = input.Floors,
                Stars = input.PropertyRating,
                TownId = input.TownId,
                ApplicationUserId = userId,
                Description = input.Description,
            };

            var rules = this.rulesRepository.AllAsNoTracking();
            var rulesIds = input.RulesIds != null ? input.RulesIds : new List<int>();
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

            Directory.CreateDirectory($"{imagePath}");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var propertyImage = new PropertyImage
                {
                    Extension = extension,
                };
                property.PropertyImages.Add(propertyImage);

                var physicalPath = $"{imagePath}{propertyImage.Id}.{extension}";
                using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                await image.CopyToAsync(fileStream);
            }

            await this.propertiesRepository.AddAsync(property);
            await this.propertiesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string propertyId, string userId)
        {
            var property = this.propertiesRepository
                .All()
                .FirstOrDefault(p => p.Id == propertyId && p.ApplicationUserId == userId);
            if (property == null)
            {
                throw new Exception("Cannot delete someone else property!");
            }

            var offers = this.offersRepository
                .All()
                .Where(o => o.PropertyId == propertyId)
                .ToList();

            if (offers != null)
            {
                foreach (var offer in offers)
                {
                    this.offersRepository.Delete(offer);
                }
            }

            this.propertiesRepository.Delete(property);
            await this.propertiesRepository.SaveChangesAsync();
            await this.offersRepository.SaveChangesAsync();
        }

        public async Task EditAsync(EditPropertyInputModel input, string userId)
        {
            var property = this.propertiesRepository
                .All()
                .FirstOrDefault(p => input.Id == p.Id && userId == p.ApplicationUserId);
            if (property == null)
            {
                throw new Exception(EditError);
            }

            property.Name = input.Name;
            property.Floors = input.Floors;
            property.Stars = input.PropertyRating;
            property.Description = input.Description;

            var propertyRules = this.propertiesRepository
                .AllWithDeleted()
                .Where(p => p.Id == input.Id)
                .Select(p => p.PropertyRules)
                .FirstOrDefault();
            var rulesIds = input.RulesIds != null ? input.RulesIds : new List<int>();
            foreach (var propertyRule in propertyRules)
            {
                propertyRule.IsAllowed = rulesIds.Contains(propertyRule.RuleId) ? true : false;
            }

            var propertyFacilities = this.propertyFacilitiesRepository
                .AllWithDeleted()
                .Where(f => f.PropertyId == input.Id)
                .ToList();
            var facilitiesIds = input.FacilitiesIds != null ? input.FacilitiesIds : new List<int>();
            foreach (var facilityId in facilitiesIds)
            {
                var isNeededToBeAdded = !propertyFacilities.Any(f => f.FacilityId == facilityId);
                if (isNeededToBeAdded)
                {
                    var propertyFacility = new PropertyFacility
                    {
                        PropertyId = input.Id,
                        FacilityId = facilityId,
                    };

                    await this.propertyFacilitiesRepository.AddAsync(propertyFacility);

                    continue;
                }
                else
                {
                    var propertyFacility = propertyFacilities.FirstOrDefault(f => f.FacilityId == facilityId);
                    if (propertyFacility.IsDeleted == true)
                    {
                        this.propertyFacilitiesRepository.Undelete(propertyFacility);

                        continue;
                    }
                }
            }

            foreach (var propertyFacility in propertyFacilities)
            {
                var isNeededToBeDeleted = !facilitiesIds.Contains(propertyFacility.FacilityId);
                if (isNeededToBeDeleted)
                {
                    this.propertyFacilitiesRepository.Delete(propertyFacility);
                }
            }

            await this.propertyFacilitiesRepository.SaveChangesAsync();
            await this.propertiesRepository.SaveChangesAsync();
        }

        public PropertiesListViewModel GetAllByUserId(string userId)
        {
            var properties = new PropertiesListViewModel
            {
                Properties = this.propertiesRepository
                    .All()
                    .Where(p => p.ApplicationUserId == userId)
                    .OrderByDescending(p => p.CreatedOn)
                    .Select(p => new PropertyInListViewModel
                    {
                        Name = p.Name,
                        Stars = p.Stars,
                        Country = p.Town.Country.Name,
                        Town = p.Town.Name,
                        PropertyCategory = p.PropertyCategory.Name,
                        Id = p.Id,
                        Image = p.PropertyImages.FirstOrDefault(pi => pi.PropertyId == p.Id) != null ?
                            $"../../images/properties/{p.PropertyImages.FirstOrDefault(pi => pi.PropertyId == p.Id).Id}.{p.PropertyImages.FirstOrDefault(pi => pi.PropertyId == p.Id).Extension}"
                            : $"../../assets/images/defaults/default.png",
                    })
                    .ToList(),
            };

            return properties;
        }

        public PropertyByIdViewModel GetPropertyAndOffersById(string propertyId, string userId)
        {
            return this.propertiesRepository
                .All()
                .Where(p => p.Id == propertyId && p.ApplicationUserId == userId)
                .Select(p => new PropertyByIdViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    Country = p.Town.Country.Name,
                    Town = p.Town.Name,
                    Description = p.Description,
                    Floors = p.Floors,
                    Stars = p.Stars,
                    PropertyCategory = p.PropertyCategory.Name,
                    PropertyType = p.PropertyCategory.PropertyType.Name,
                    CurrencyCode = p.Town.Country.Currency.CurrencyCode,
                    Facilities = p.PropertyFacilities
                        .Select(f => f.Facility.Name)
                        .ToList(),
                    Offers = p.Offers
                        .Select(o => new OfferViewModel
                        {
                            Id = o.Id,
                            Price = o.PricePerPerson,
                            ValidFrom = o.ValidFrom.ToString("dd/MM/yyyy"),
                            ValidTo = o.ValidTo.ToString("dd/MM/yyyy"),
                            OfferFacilities = o.OfferFacilities
                                .Select(f => new OfferFacilityViewModel
                                {
                                    Name = f.Facility.Name,
                                    Category = f.Facility.FacilityCategory.Name,
                                })
                                .ToList(),
                            Rooms = o.OfferBedTypes.Select(b => b.BedType.Type).ToList(),
                            Guests = (byte)o.OfferBedTypes.Sum(b => b.BedType.Capacity),
                            Images = o.OfferImages.Select(oi => "/images/offers/" + oi.Id + "." + oi.Extension).ToList(),
                        })
                        .ToList(),
                })
                .FirstOrDefault();
        }

        public EditPropertyInputModel GetById(string propertyId, string userId)
        {
            var property = this.propertiesRepository
                .All()
                .Where(p => p.Id == propertyId && p.ApplicationUserId == userId)
                .Select(p => new EditPropertyInputModel
                {
                    Name = p.Name,
                    Description = p.Description,
                    PropertyRating = p.Stars,
                    Floors = p.Floors,
                    Id = p.Id,
                })
                .FirstOrDefault();

            if (property == null)
            {
                throw new Exception(EditError);
            }

            return property;
        }

        public string GetIdByName(string propertyName)
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

        public string GetIdByOfferId(string id)
        {
            return this.propertiesRepository
                .All()
                .FirstOrDefault(p => p.Offers.Any(o => o.Id == id))
                .Id;
        }
    }
}
