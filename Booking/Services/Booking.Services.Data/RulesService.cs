﻿namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;
    using Booking.Web.ViewModels.PropertyRules;
    using Booking.Web.ViewModels.Rules;

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

        public IEnumerable<RuleInputModel> GetAll()
        {
            return this.rulesRepository
                .All()
                .Select(r => new RuleInputModel
                {
                    Id = r.Id,
                    Name = r.Name,
                })
                .ToList();
        }

        public IEnumerable<PropertyRuleViewModel> GetAllByPropertyId(string id)
        {
            return this.propertyRulesRepository
                .All()
                .Where(r => r.PropertyId == id)
                .Select(r => new PropertyRuleViewModel
                {
                    Id = r.RuleId,
                    Name = r.Rule.Name,
                    IsAllowed = r.IsAllowed,
                })
                .ToList();
        }
    }
}
