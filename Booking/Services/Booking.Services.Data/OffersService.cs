namespace Booking.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Offers;

    public class OffersService : IOffersService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };
        private readonly IDeletableEntityRepository<Offer> offersRepository;
        private readonly IRepository<BedType> bedTypesRepository;
        private readonly IDeletableEntityRepository<OfferBedType> offerBedTypesRepository;
        private readonly IDeletableEntityRepository<OfferFacility> offerFacilitiesRepository;
        private readonly IRepository<OfferImage> offerImagesRepository;

        public OffersService(
            IDeletableEntityRepository<Offer> offersRepository,
            IRepository<BedType> bedTypesRepository,
            IDeletableEntityRepository<OfferBedType> offerBedTypesRepository,
            IDeletableEntityRepository<OfferFacility> offerFacilitiesRepository,
            IRepository<OfferImage> offerImagesRepository)
        {
            this.offersRepository = offersRepository;
            this.bedTypesRepository = bedTypesRepository;
            this.offerBedTypesRepository = offerBedTypesRepository;
            this.offerFacilitiesRepository = offerFacilitiesRepository;
            this.offerImagesRepository = offerImagesRepository;
        }

        public async Task AddToProperty(AddOfferInputModel input, string imagePath)
        {
            var offer = new Offer
            {
                ValidTo = (DateTime)input.ValidTo,
                ValidFrom = (DateTime)input.ValidFrom,
                PropertyId = input.PropertyId,
                PricePerPerson = input.PricePerPerson,
            };

            var index = 0;
            var count = 0;
            var bedTypesIds = this.bedTypesRepository
                .All()
                .OrderBy(b => b.Type)
                .Select(b => b.Id)
                .ToList();
            var bedTypesDb = this.bedTypesRepository
                .All()
                .Select(b => new
                {
                    b.Id,
                    b.Capacity,
                })
                .ToList();
            foreach (var bedTypeId in bedTypesIds)
            {
                var id = bedTypeId;
                var bedTypeCount = input.BedTypesCounts.ToList()[index];
                if (bedTypeCount > 0)
                {
                    for (int i = 0; i < bedTypeCount; i++)
                    {
                        var offerBedType = new OfferBedType
                        {
                            BedTypeId = id,
                            Offer = offer,
                        };
                        offer.OfferBedTypes.Add(offerBedType);
                    }

                    count += bedTypesDb.First(x => x.Id == bedTypeId).Capacity * bedTypeCount;
                    if (count > 30)
                    {
                        throw new Exception("Cannot have more than 30 members in an offer!");
                    }
                }

                index++;
            }

            var facilitiesIds = input.OfferFacilitiesIds;
            if (facilitiesIds != null)
            {
                foreach (var facilityId in facilitiesIds)
                {
                    var offerFacility = new OfferFacility
                    {
                        Offer = offer,
                        FacilityId = facilityId,
                    };

                    offer.OfferFacilities.Add(offerFacility);
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

                    var offerImage = new OfferImage
                    {
                        Extension = extension,
                    };
                    offer.OfferImages.Add(offerImage);

                    var physicalPath = $"{imagePath}{offerImage.Id}.{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.offersRepository.AddAsync(offer);
            await this.offersRepository.SaveChangesAsync();
        }

        public async Task DeleteAllByPropertyIdAsync(string propertyId, string userId, string imagePath)
        {
            var offersIds = this.offersRepository
                .All()
                .Where(o => o.PropertyId == propertyId && userId == o.Property.ApplicationUserId)
                .Select(o => o.Id)
                .ToList();
            if (offersIds.Any() && offersIds != null)
            {
                foreach (var offerId in offersIds)
                {
                    await this.DeleteAsync(offerId, userId, imagePath);
                }
            }
        }

        public async Task DeleteAsync(string offerId, string userId, string imagePath)
        {
            var offer = this.offersRepository
                .All()
                .FirstOrDefault(o => o.Id == offerId && o.Property.ApplicationUserId == userId);
            if (offer == null)
            {
                throw new Exception(GlobalConstants.ErrorMessages.OfferAccessValue);
            }

            var bedTypes = this.offerBedTypesRepository
                .All()
                .Where(o => o.OfferId == offerId)
                .ToList();
            foreach (var bedType in bedTypes)
            {
                this.offerBedTypesRepository.Delete(bedType);
            }

            var facilities = this.offerFacilitiesRepository
                .All()
                .Where(o => o.OfferId == offerId)
                .ToList();
            foreach (var facility in facilities)
            {
                this.offerFacilitiesRepository.Delete(facility);
            }

            var images = this.offerImagesRepository
                .All()
                .Where(o => o.OfferId == offerId)
                .ToList();
            foreach (var image in images)
            {
                var fileName = $"{imagePath}{image.Id}.{image.Extension}";
                this.offerImagesRepository.Delete(image);
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }

            this.offersRepository.Delete(offer);
            await this.offerBedTypesRepository.SaveChangesAsync();
            await this.offerFacilitiesRepository.SaveChangesAsync();
            await this.offerImagesRepository.SaveChangesAsync();
            await this.offersRepository.SaveChangesAsync();
        }

        public EditOfferViewModel GetById(string id, string userId)
        {
            var offer = this.offersRepository
                .All()
                .Where(o => o.Id == id && o.Property.ApplicationUserId == userId)
                .Select(o => new EditOfferViewModel
                {
                    PricePerPerson = o.PricePerPerson,
                    ValidTo = o.ValidTo,
                    ValidFrom = o.ValidFrom,
                    OfferId = o.Id,
                })
                .FirstOrDefault();
            if (offer == null)
            {
                throw new Exception(GlobalConstants.ErrorMessages.OfferAccessValue);
            }

            return offer;
        }

        public async Task UpdateAsync(string userId, EditOfferViewModel input)
        {
            var offer = this.offersRepository
                .All()
                .FirstOrDefault(o => o.Id == input.OfferId && o.Property.ApplicationUserId == userId);
            if (offer == null)
            {
                throw new Exception(GlobalConstants.ErrorMessages.OfferAccessValue);
            }

            offer.PricePerPerson = input.PricePerPerson;
            offer.ValidFrom = (DateTime)input.ValidFrom;
            offer.ValidTo = (DateTime)input.ValidTo;

            await this.offersRepository.SaveChangesAsync();
        }
    }
}
