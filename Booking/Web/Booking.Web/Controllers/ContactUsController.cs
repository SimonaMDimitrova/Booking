namespace Booking.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ContactUsController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
