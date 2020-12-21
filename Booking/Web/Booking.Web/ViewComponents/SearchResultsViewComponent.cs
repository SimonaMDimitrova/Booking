namespace Booking.Web.ViewComponents
{
    using Booking.Services.Data;
    using Booking.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class SearchResultsViewComponent : ViewComponent
    {
        private readonly IPropertiesService propertiesService;

        public SearchResultsViewComponent(IPropertiesService propertiesService)
        {
            this.propertiesService = propertiesService;
        }

        public IViewComponentResult Invoke(IndexInputModel input)
        {
            var viewModel = this.propertiesService.GetBySearchRequirements(input);

            return this.View(viewModel);
        }
    }
}
