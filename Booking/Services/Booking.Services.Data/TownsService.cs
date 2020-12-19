namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;

    public class TownsService : ITownsService
    {
        private readonly IRepository<Town> townsRepository;

        public TownsService(IRepository<Town> townsRepository)
        {
            this.townsRepository = townsRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairBasedOnCountryId(int id)
        {
            return this.townsRepository
                .All()
                .Where(t => t.CountryId == id)
                .Select(t => new
                {
                    t.Name,
                    t.Id,
                })
                .OrderBy(t => t.Name)
                .ToList().Select(t => new KeyValuePair<string, string>(t.Id.ToString(), t.Name));
        }

        public IEnumerable<KeyValuePair<string, string>> GetMostPopularByKeyValuePairBasedOnCountryId(int id)
        {
            return this.townsRepository
                .All()
                .Where(t => t.CountryId == id && t.Properties.Any(p => p.Offers.Count > 0))
                .Select(t => new
                {
                    t.Name,
                    t.Id,
                })
                .OrderBy(t => t.Name)
                .ToList().Select(t => new KeyValuePair<string, string>(t.Id.ToString(), t.Name));
        }
    }
}
