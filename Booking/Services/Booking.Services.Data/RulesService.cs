namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Services.Mapping;

    public class RulesService : IRulesService
    {
        private readonly IRepository<Rule> rulesRepository;
        private readonly IDeletableEntityRepository<PropertyRule> propertyRulesRepository;

        public RulesService(
            IDeletableEntityRepository<PropertyRule> propertyRulesRepository,
            IRepository<Rule> rulesRepository)
        {
            this.rulesRepository = rulesRepository;
            this.propertyRulesRepository = propertyRulesRepository;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.rulesRepository
                .All()
                .OrderBy(r => r.Name)
                .To<T>()
                .ToList();
        }

        public IEnumerable<T> GetAllByPropertyId<T>(string id)
        {
            return this.propertyRulesRepository
                .All()
                .Where(r => r.PropertyId == id)
                .OrderBy(r => r.Rule.Name)
                .To<T>()
                .ToList();
        }
    }
}
