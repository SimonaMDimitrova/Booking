namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.PropertiesViewModels.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.Edit;
    using Booking.Web.ViewModels.Rules;

    public interface IRulesService
    {
        IEnumerable<RuleInputModel> GetAll();

        IEnumerable<EditRuleInputModel> GetAllByPropertyId(string id);
    }
}
