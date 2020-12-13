namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Offers;

    public class OffersService : IOffersService
    {
        private readonly IDeletableEntityRepository<Offer> offersRepository;
        private readonly IRepository<BedType> bedTypesRepository;

        public OffersService(IDeletableEntityRepository<Offer> offersRepository, IRepository<BedType> bedTypesRepository)
        {
            this.offersRepository = offersRepository;
            this.bedTypesRepository = bedTypesRepository;
        }

        public async Task AddOfferToProperty(string propertyId, AddOfferInputModel input)
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

            await this.offersRepository.AddAsync(offer);
            await this.offersRepository.SaveChangesAsync();
        }

        public async Task AddToUserBookingList(string offerId, string userId)
        {
            var offer = this.offersRepository.All().FirstOrDefault(o => o.Id == offerId);
            var applicationUserOffer = new ApplicationUserOffer
            {
                ApplicationUserId = userId,
                OfferId = offerId,
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
