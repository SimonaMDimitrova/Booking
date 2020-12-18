namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;

    public class BedTypesService : IBedTypesService
    {
        private readonly IRepository<BedType> bedTypesRepository;

        public BedTypesService(IRepository<BedType> bedTypesRepository)
        {
            this.bedTypesRepository = bedTypesRepository;
        }

        public IEnumerable<KeyValuePair<int, string>> GetAll()
        {
            return this.bedTypesRepository
                .All()
                .Select(b => new
                {
                    Id = b.Id,
                    Type = b.Type,
                })
                .OrderBy(b => b.Type)
                .ToList().Select(b => new KeyValuePair<int, string>(b.Id, b.Type));
        }
    }
}
