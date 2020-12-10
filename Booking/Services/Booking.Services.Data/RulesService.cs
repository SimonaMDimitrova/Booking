namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.Rules;

    public class RulesService : IRulesService
    {
        private readonly IRepository<Rule> rulesRepository;

        public RulesService(IRepository<Rule> rulesRepository)
        {
            this.rulesRepository = rulesRepository;
        }

        public IEnumerable<RuleIdNameViewModel> GetAllRules()
        {
            return this.rulesRepository
                .AllAsNoTracking()
                .Select(r => new RuleIdNameViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                })
                .ToList();
        }
    }
}
