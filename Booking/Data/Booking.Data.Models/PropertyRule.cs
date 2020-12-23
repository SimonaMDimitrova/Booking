namespace Booking.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using global::Booking.Data.Common.Models;

    public class PropertyRule : BaseDeletableModel<int>
    {
        [Required]
        public string PropertyId { get; set; }

        public virtual Property Property { get; set; }

        [Required]
        public int RuleId { get; set; }

        public virtual Rule Rule { get; set; }

        public bool IsAllowed { get; set; }
    }
}
