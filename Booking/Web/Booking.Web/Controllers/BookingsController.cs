namespace Booking.Web.Controllers
{
    using System.Threading.Tasks;

    using Booking.Data.Models;
    using Booking.Services.Data;
    using Booking.Web.ViewModels.Bookings;
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
        public async Task<IActionResult> All()
        {
            var viewModel = new BookingInListViewModel();

            var user = await this.userManager.GetUserAsync(this.User);
            viewModel.Bookings = this.offersService.GetBookingsByUserId(user.Id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Book(BookingInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.offersService.AddToUserBookingList(input, user.Id);
            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.offersService.DeleteBookingAsync(id, user.Id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
