namespace Booking.Web.InputModels.PropertiesInputModels.Edit
{
    using Booking.Data.Models;
    using Booking.Services.Mapping;

    public class EditRuleInputModel : IMapFrom<PropertyRule>
    {
        public int RuleId { get; set; }

        public string RuleName { get; set; }

        public bool IsAllowed { get; set; }
    }
}
