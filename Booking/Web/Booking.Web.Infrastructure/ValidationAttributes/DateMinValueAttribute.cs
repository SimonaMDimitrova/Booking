namespace Booking.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Booking.Common;

    public class DateMinValueAttribute : ValidationAttribute
    {
        public DateMinValueAttribute()
        {
            this.ErrorMessage = GlobalConstants.ErrorMessages.OfferValidFrom;
        }

        public override bool IsValid(object value)
        {
            if (value is DateTime)
            {
                var date = (DateTime)value;

                if (date >= DateTime.UtcNow.Date)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
