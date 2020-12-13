﻿namespace Booking.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;

    public class SearchIndexInputModel
    {
        public IEnumerable<KeyValuePair<string, string>> Countries { get; set; }

        public string CountryId { get; set; }

        public string TownId { get; set; }

        public DateTime? CheckIn { get; set; }

        public DateTime? CheckOut { get; set; }

        public int Members { get; set; }

        public decimal MinBudget { get; set; }

        public decimal MaxBudget { get; set; }
    }
}