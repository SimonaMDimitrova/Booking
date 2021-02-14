namespace Booking.Web.InputModels.PropertiesInputModels.Edit
{
    using Booking.Data.Models;
    using Booking.Services.Mapping;

    public class EditRuleInputModel : IMapFrom<PropertyRule>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAllowed { get; set; }
    }
}
