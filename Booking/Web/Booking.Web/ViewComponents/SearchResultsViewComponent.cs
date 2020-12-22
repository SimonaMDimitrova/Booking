namespace Booking.Web.ViewComponents
{
    using Booking.Data.Models;
    using Booking.Services.Data;
    using Booking.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class SearchResultsViewComponent : ViewComponent
    {
        private readonly IPropertiesService propertiesService;

        public SearchResultsViewComponent(IPropertiesService propertiesService)
        {
            this.propertiesService = propertiesService;
        }

        public IViewComponentResult Invoke(IndexInputModel input, string userEmail)
        {
            var viewModel = this.propertiesService.GetBySearchRequirements(input, userEmail);

            return this.View(viewModel);
        }
    }
}
