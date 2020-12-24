namespace Booking.Web.ViewComponents
{
    using Booking.Services.Data;
    using Booking.Web.ViewModels.ViewComponents.Countries;
    using Microsoft.AspNetCore.Mvc;

    public class CountriesNamesViewComponent : ViewComponent
    {
        private readonly ICountriesService countriesService;

        public CountriesNamesViewComponent(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IViewComponentResult Invoke()
        {
            var countries = this.countriesService.GetTheSixMostVisitedNames();
            var viewModel = new CountriesNamesViewModel
            {
                Countries = countries,
            };

            return this.View(viewModel);
        }
    }
}
