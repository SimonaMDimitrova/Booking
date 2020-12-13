﻿namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.PropertyRules;
    using Booking.Web.ViewModels.Rules;

    public interface IRulesService
    {
        IEnumerable<RuleIdNameViewModel> GetAllRules();

        IEnumerable<PropertyRuleViewModel> GetAllRulesByPropertyId(string id);
    }
}