namespace Booking.Web.Controllers
{
    using Booking.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
