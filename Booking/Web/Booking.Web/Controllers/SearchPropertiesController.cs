namespace Booking.Web.Controllers
{
    using Booking.Services.Data;
    using Booking.Web.ViewModels.Home;
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

            //var viewModel = this.propertiesService.GetBySearchRequirements(input);
            return this.View(input);
        }
    }
}
