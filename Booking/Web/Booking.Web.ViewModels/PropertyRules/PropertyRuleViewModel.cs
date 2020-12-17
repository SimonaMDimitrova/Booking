namespace Booking.Web.ViewModels.PropertyRules
{
    using Booking.Web.ViewModels.Rules;

    public class PropertyRuleViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsAllowed { get; set; }
    }
}
