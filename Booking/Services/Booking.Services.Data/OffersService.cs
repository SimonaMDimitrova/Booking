namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Bookings;
    using Booking.Web.ViewModels.Offers;

    public class OffersService : IOffersService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png" };
        private readonly IDeletableEntityRepository<Offer> offersRepository;
        private readonly IRepository<BedType> bedTypesRepository;
        private readonly IDeletableEntityRepository<ApplicationUserOffer> applicationUserOfferRepository;

        public OffersService(
            IDeletableEntityRepository<Offer> offersRepository,
            IRepository<BedType> bedTypesRepository,
            IDeletableEntityRepository<ApplicationUserOffer> applicationUserOfferRepository)
        {
            this.offersRepository = offersRepository;
            this.bedTypesRepository = bedTypesRepository;
            this.applicationUserOfferRepository = applicationUserOfferRepository;
        }

        public async Task AddToProperty(string propertyId, AddOfferInputModel input, string imagePath)
        {
            var offer = new Offer
            {
                ValidTo = (DateTime)input.ValidTo,
                ValidFrom = (DateTime)input.ValidFrom,
                PropertyId = propertyId,
                PricePerPerson = input.PricePerPerson,
            };

            var index = 0;
            var bedTypesIds = this.bedTypesRepository
                .All()
                .OrderBy(b => b.Type)
                .Select(b => b.Id)
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

        public async Task AddToUserBookingList(BookingInputModel input, string userId)
        {
            var offer = this.offersRepository.All().FirstOrDefault(o => o.Id == input.OfferId);
            var applicationUserOffer = new ApplicationUserOffer
            {
                ApplicationUserId = userId,
                OfferId = input.OfferId,
                CheckIn = input.CheckIn,
                CheckOut = input.CheckOut,
            };

            offer.ApplicationUserOffers.Add(applicationUserOffer);
            await this.offersRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var offer = this.offersRepository
                .All()
                .FirstOrDefault(o => o.Id == id);
            this.offersRepository.Delete(offer);
            await this.offersRepository.SaveChangesAsync();
        }

        public async Task DeleteBookingAsync(string bookingId, string userId)
        {
            var booking = this.applicationUserOfferRepository
                .All()
                .Where(a => a.ApplicationUserId == userId && a.Id == bookingId)
                .FirstOrDefault();

            this.applicationUserOfferRepository.Delete(booking);
            await this.applicationUserOfferRepository.SaveChangesAsync();
        }

        public IEnumerable<BookingViewModel> GetBookingsByUserId(string userId)
        {
            var bookings = this.applicationUserOfferRepository
                .All()
                .Where(a => a.ApplicationUserId == userId)
                .Select(a => new BookingViewModel
                {
                    Id = a.Id,
                    Address = a.Offer.Property.Address,
                    Country = a.Offer.Property.Town.Country.Name,
                    Town = a.Offer.Property.Town.Name,
                    CurrencyCode = a.Offer.Property.Town.Country.Currency.CurrencyCode,
                    PropertyName = a.Offer.Property.Name,
                    Members = a.Offer.OfferBedTypes.Sum(b => b.BedType.Capacity),
                    Price = a.Offer.PricePerPerson * a.Offer.OfferBedTypes.Sum(b => b.BedType.Capacity),
                })
                .ToList();

            return bookings;
        }

        public EditOfferViewModel GetById(string id)
        {
            return this.offersRepository
                .All()
                .Where(o => o.Id == id)
                .Select(o => new EditOfferViewModel
                {
                    PricePerPerson = o.PricePerPerson,
                    ValidTo = o.ValidTo,
                    ValidFrom = o.ValidFrom,
                })
                .FirstOrDefault();
        }

        public async Task UpdateAsync(string offerId, EditOfferViewModel input)
        {
            var offer = this.offersRepository.All().FirstOrDefault(o => o.Id == offerId);
            offer.PricePerPerson = input.PricePerPerson;
            offer.ValidFrom = (DateTime)input.ValidFrom;
            offer.ValidTo = (DateTime)input.ValidTo;

            await this.offersRepository.SaveChangesAsync();
        }
    }
}
