namespace Booking.Web.Controllers
{
    using Booking.Services.Data;
    using Booking.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class SearchPropertiesController : BaseController
    {
        private readonly IPropertiesService propertiesService;

        public SearchPropertiesController(IPropertiesService propertiesService)
        {
            this.propertiesService = propertiesService;
        }

        public IActionResult Index(SearchIndexInputModel input)
        {
            var viewModel = this.propertiesService.GetBySearchRequirements(input);
            return this.View(viewModel);
        }
    }
}
