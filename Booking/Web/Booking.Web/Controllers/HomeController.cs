﻿namespace Booking.Web.Controllers
{
    using System.Diagnostics;

    using Booking.Services.Data;
    using Booking.Web.ViewModels;
    using Booking.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : PropertiesBaseController
    {
        private readonly ICountriesService countriesService;

        public HomeController(
            ICountriesService countriesService,
            ITownsService townsService,
            ICurrenciesService currenciesService)
            : base(townsService, currenciesService)
        {
            this.countriesService = countriesService;
        }

        public IActionResult Index()
        {
            var countries = this.countriesService.GetMostPopularByKeyValuePairs();

            var input = new IndexInputModel
            {
                Countries = countries,
            };

            return this.View(input);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult NotFoundError()
        {
            return this.View();
        }
    }
}
