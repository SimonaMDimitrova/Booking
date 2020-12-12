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
                PricePerPerson = input.Price,
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
    }
}
