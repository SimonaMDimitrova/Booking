namespace Booking.Web.Controllers
{
    using Booking.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public abstract class PropertiesBaseController : BaseController
    {
        private readonly ITownsService townsService;
        private readonly ICurrenciesService currenciesService;

        public PropertiesBaseController(ITownsService townsService, ICurrenciesService currenciesService)
        {
            this.townsService = townsService;
            this.currenciesService = currenciesService;
        }

        public IActionResult GetTowns(string id)
        {
            return this.Json(this.townsService.GetAllByKeyValuePairBasedOnCountryId(int.Parse(id)));
        }

        public IActionResult GetCurrencyCode(string id)
        {
            return this.Json(this.currenciesService.GetByCountryId(int.Parse(id)));
        }

        public IActionResult GetMostPopularTowns(string id)
        {
            return this.Json(this.townsService.GetMostPopularByKeyValuePairBasedOnCountryId(int.Parse(id)));
        }
    }
}
