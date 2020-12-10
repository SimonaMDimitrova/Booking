namespace Booking.Web.ViewComponents
{
    using System.Linq;

    using Booking.Services.Data;
    using Booking.Web.ViewModels.ViewComponents.TopCountries;
    using Microsoft.AspNetCore.Mvc;

    public class TopCountriesViewComponent : ViewComponent
    {
        private readonly ICountriesService countriesService;

        public TopCountriesViewComponent(ICountriesService countriesService)
        {
            this.countriesService = countriesService;
        }

        public IViewComponentResult Invoke()
        {
            var countriesDto = this.countriesService.GetTheSixTopCountries()
                .Select(c => new CountryViewModel
                {
                    Name = c.Name,
                    OffersCount = c.OffersCount,
                    BookingsCount = c.BookingsCount,
                })
                .ToList();
            var viewModel = new CountryInListViewModel
            {
                Countries = countriesDto,
            };

            return this.View(viewModel);
        }
    }
}
