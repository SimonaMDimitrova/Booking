﻿namespace Booking.Web.Infrastructure.ValidationAttributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class DateMinValueAttribute : ValidationAttribute
    {
        public DateMinValueAttribute()
        {
            this.ErrorMessage = "The date cannot be before today.";
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