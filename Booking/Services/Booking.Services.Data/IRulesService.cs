namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.ViewModels.PropertiesViewModels.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.Edit;

    public interface IRulesService
    {
        IEnumerable<RuleInputModel> GetAll();

        IEnumerable<EditRuleInputModel> GetAllByPropertyId(string id);
    }
}
