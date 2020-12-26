namespace Booking.Web.Controllers
{
    using System;
    using Booking.Common;
    using Booking.Services.Data;
    using Booking.Web.ViewModels.Home;
    using Booking.Web.ViewModels.SearchProperties;
    using Microsoft.AspNetCore.Mvc;

    public class SearchPropertiesController : BaseController
    {
        private readonly IPropertiesService propertiesService;
        private readonly ICountriesService countriesService;

        public SearchPropertiesController(
            IPropertiesService propertiesService,
            ICountriesService countriesService)
        {
            this.propertiesService = propertiesService;
            this.countriesService = countriesService;
        }

        public IActionResult Index(IndexInputModel input)
        {
            var countries = this.countriesService.GetMostPopularByKeyValuePairs();
            input.Countries = countries;

            return this.View(input);
        }

        public IActionResult ById(SearchedInputModel input)
        {
            var viewModel = this.propertiesService.GetByIdBasedOnSearchRequirements(input);

            if (viewModel == null || !this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index), new IndexInputModel());
            }

            return this.View(viewModel);
        }
    }
}
