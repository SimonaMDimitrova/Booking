namespace Booking.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Booking.Services.Data;
    using Booking.Web.ViewModels;
    using Booking.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly ICountriesService countriesService;
        private readonly ITownsService townsService;

        public HomeController(ICountriesService countriesService, ITownsService townsService)
        {
            this.countriesService = countriesService;
            this.townsService = townsService;
        }

        public IActionResult Index()
        {
            var countries = this.countriesService.GetAllByKeyValuePairs();
            var viewModel = new IndexInputModel
            {
                Countries = countries,
            };

            return this.View(viewModel);
        }

        public IActionResult GetTowns(string id)
        {
            return this.Json(this.townsService.GetTownsByCountryId(int.Parse(id)));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
