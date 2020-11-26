namespace Booking.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class Imgs : BaseController
    {
        public IActionResult Image()
        {
            return this.View();
        }
    }
}
