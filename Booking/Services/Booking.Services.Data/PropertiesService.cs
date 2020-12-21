﻿namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.BedTypes;
    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.Home;
    using Booking.Web.ViewModels.Offers;
    using Booking.Web.ViewModels.PropertiesViewModels;
    using Booking.Web.ViewModels.Rules;
    using Booking.Web.ViewModels.SearchProperties;

    public class PropertiesService : IPropertiesService
    {
        private const string Error = "You don't have permission to make any changes to this property (or it doesn't exists).";

        private readonly string[] allowedExtensions = new[] { "jpg", "png" };
        private readonly IDeletableEntityRepository<Property> propertiesRepository;
        private readonly IRepository<Rule> rulesRepository;
        private readonly IDeletableEntityRepository<Offer> offersRepository;
        private readonly IDeletableEntityRepository<PropertyFacility> propertyFacilitiesRepository;
        private readonly IRepository<PropertyImage> propertyImagesRepository;
        private readonly IDeletableEntityRepository<PropertyRule> propertyRulesRepository;

        public PropertiesService(
            IDeletableEntityRepository<Property> propertiesRepository,
            IRepository<Rule> rulesRepository,
            IDeletableEntityRepository<Offer> offersRepository,
            IDeletableEntityRepository<PropertyFacility> propertyFacilitiesRepository,
            IRepository<PropertyImage> propertyImagesRepository,
            IDeletableEntityRepository<PropertyRule> propertyRulesRepository)
        {
            this.propertiesRepository = propertiesRepository;
            this.rulesRepository = rulesRepository;
            this.offersRepository = offersRepository;
            this.propertyFacilitiesRepository = propertyFacilitiesRepository;
            this.propertyImagesRepository = propertyImagesRepository;
            this.propertyRulesRepository = propertyRulesRepository;
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

        public SearchIndexListViewModel GetBySearchRequirements(IndexInputModel input)
        {
            var isCountryIdParsed = int.TryParse(input.CountryId, out int countryId);
            var isTownIdParsed = int.TryParse(input.TownId, out int townId);
            var isCheckInValid = DateTime.TryParse(input.CheckIn.ToString(), out var checkIn);
            var isCheckOutValid = DateTime.TryParse(input.CheckOut.ToString(), out var checkOut);

            if (
                !isCheckInValid
                || !isCheckOutValid
                || !isCountryIdParsed
                || !isTownIdParsed
                || countryId <= 0
                || townId <= 0
                || input.Members <= 0
                || input.MinBudget < 0
                || input.MinBudget > input.MaxBudget)
            {
                return null;
            }

            var properties = this.propertiesRepository
                .All()
                .Where(
                    p => p.TownId == townId
                    && p.Town.CountryId == countryId)
                .Select(p => new SearchIndexInListViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Address = p.Address,
                    Country = p.Town.Country.Name,
                    Town = p.Town.Name,
                    Description = p.Description,
                    Floors = p.Floors,
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    Stars = p.Stars,
                    PropertyCategory = p.PropertyCategory.Name,
                    PropertyType = p.PropertyCategory.PropertyType.Name,
                    CurrencyCode = p.Town.Country.Currency.CurrencyCode,
                    Facilities = p.PropertyFacilities
                        .Select(f => f.Facility.Name)
                        .ToList(),
                    Offers = p.Offers
                        .Where(
                            o => (o.ValidFrom.Date >= DateTime.UtcNow.Date ? o.ValidFrom.Date : DateTime.UtcNow.Date) <= checkIn.Date
                                && o.ValidTo.Date <= checkOut.Date
                                && (checkOut - checkIn).TotalDays >= 2)
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
                                Rooms = o.OfferBedTypes
                                    .Select(b => new BedTypeViewModel
                                    {
                                        Type = b.BedType.Type,
                                        Capacity = b.BedType.Capacity,
                                    })
                                    .ToList(),
                                Guests = (byte)o.OfferBedTypes.Sum(b => b.BedType.Capacity),
                            })
                            .Where(
                                o => o.Guests == input.Members
                                && (o.Price >= input.MinBudget && o.Price <= (input.MaxBudget == 0 ? int.MaxValue : input.MaxBudget)))
                            .ToList(),
                })
                .ToList();

            if (properties == null)
            {
                return null;
            }

            var propertiesViewModel = new SearchIndexListViewModel();
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
            if (input.Images != null && input.Images.Any())
            {
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
            }

            await this.propertiesRepository.AddAsync(property);
            await this.propertiesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string propertyId, string userId, string imagePath)
        {
            var property = this.propertiesRepository
                .All()
                .FirstOrDefault(p => p.Id == propertyId && p.ApplicationUserId == userId);
            if (property == null)
            {
                throw new Exception("Cannot delete someone else property!");
            }

            var rules = this.propertyRulesRepository
                .All()
                .Where(r => r.PropertyId == propertyId)
                .ToList();
            foreach (var rule in rules)
            {
                this.propertyRulesRepository.Delete(rule);
            }

            var images = this.propertyImagesRepository
                .All()
                .Where(p => p.PropertyId == propertyId)
                .ToList();
            foreach (var image in images)
            {
                var fileName = $"{imagePath}{image.Id}.{image.Extension}";
                this.propertyImagesRepository.Delete(image);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }

            this.propertiesRepository.Delete(property);
            await this.propertiesRepository.SaveChangesAsync();
            await this.propertyImagesRepository.SaveChangesAsync();
            await this.propertyRulesRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(EditPropertyInputModel input, string userId)
        {
            var property = this.propertiesRepository
                .All()
                .FirstOrDefault(p => input.Id == p.Id && userId == p.ApplicationUserId);
            if (property == null)
            {
                throw new Exception(Error);
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
                            : p.Offers.Select(o => o.OfferImages.FirstOrDefault()).FirstOrDefault() != null ?
                            $"../../images/offers/{p.Offers.Select(o => o.OfferImages.FirstOrDefault()).FirstOrDefault().Id}.{p.Offers.Select(o => o.OfferImages.FirstOrDefault()).FirstOrDefault().Extension}"
                            : "../../assets/images/defaults/default.png",
                    })
                    .ToList(),
            };

            return properties;
        }

        public PropertyByIdViewModel GetPropertyAndOffersById(string propertyId, string userId)
        {
            var property = this.propertiesRepository
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
                    Rules = p.PropertyRules
                        .Select(r => new RuleNameIsAvailableViewModel
                        {
                            Name = r.Rule.Name,
                            IsAllowed = r.IsAllowed,
                        })
                        .ToList(),
                    Images = p.PropertyImages.Select(oi => "/images/properties/" + oi.Id + "." + oi.Extension).ToList(),
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
                            Rooms = o.OfferBedTypes
                                .Select(b => new BedTypeViewModel
                                {
                                    Type = b.BedType.Type,
                                    Capacity = b.BedType.Capacity,
                                })
                                .ToList(),
                            Guests = (byte)o.OfferBedTypes.Sum(b => b.BedType.Capacity),
                            Images = o.OfferImages.Select(oi => "/images/offers/" + oi.Id + "." + oi.Extension).ToList(),
                        })
                        .ToList(),
                })
                .FirstOrDefault();

            return property;
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

        public string GetIdByOfferId(string id, string userId)
        {
            var property = this.propertiesRepository
                .All()
                .FirstOrDefault(p => p.Offers.Any(o => o.Id == id) && p.ApplicationUserId == userId);
            if (property == null)
            {
                throw new Exception(Error);
            }

            return property.Id;
        }

        public string GetNameById(string id)
        {
            return this.propertiesRepository
                .All()
                .FirstOrDefault(p => p.Id == id)
                .Name;
        }
    }
}
