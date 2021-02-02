namespace Booking.Services.Data
{
    using System.Collections.Generic;

    using Booking.Web.InputModels.PropertiesInputModels.Add;
    using Booking.Web.InputModels.PropertiesInputModels.Edit;

    public interface IRulesService
    {
        IEnumerable<RuleInputModel> GetAll();

        IEnumerable<EditRuleInputModel> GetAllByPropertyId(string id);
    }
}
