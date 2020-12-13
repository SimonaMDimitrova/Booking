namespace Booking.Web.Controllers
{
    using System.Threading.Tasks;

    using Booking.Data.Models;
    using Booking.Services.Data;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BookingsController : Controller
    {
        private readonly IOffersService offersService;
        private readonly UserManager<ApplicationUser> userManager;

        public BookingsController(
            IOffersService offersService,
            UserManager<ApplicationUser> userManager)
        {
            this.offersService = offersService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult All()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Book(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.offersService.AddToUserBookingList(id, user.Id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
