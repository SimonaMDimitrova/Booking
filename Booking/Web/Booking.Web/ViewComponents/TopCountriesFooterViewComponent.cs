namespace Booking.Web.ViewComponents
{
    using System.Linq;

    using Booking.Services.Data;
    using Booking.Web.ViewModels.ViewComponents.TopCountries;
    using Microsoft.AspNetCore.Mvc;

    public class TopCountriesFooterViewComponent : ViewComponent
    {
        private readonly ICountriesService countriesService;

        public TopCountriesFooterViewComponent(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IViewComponentResult Invoke()
        {
            var countries = this.countriesService.GetTheSixTopCountries().Select(c => c.Name).ToList();
            var viewModel = new CountryByNameInListViewModel
            {
                Names = countries,
            };

            return this.View(viewModel);
        }
    }
}
