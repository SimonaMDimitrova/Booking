namespace Booking.Web.ViewComponents
{
    using Booking.Services.Data;
    using Booking.Web.ViewModels.ViewComponents.Countries;
    using Microsoft.AspNetCore.Mvc;

    public class CountriesViewComponent : ViewComponent
    {
        private readonly ICountriesService countriesService;

        public CountriesViewComponent(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IViewComponentResult Invoke()
        {
            var countries = this.countriesService.GetMostPopular();
            var viewModel = new CountriesListViewModel
            {
                Countries = countries,
            };

            return this.View(viewModel);
        }
    }
}
