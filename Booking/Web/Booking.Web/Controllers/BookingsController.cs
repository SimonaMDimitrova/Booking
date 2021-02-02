namespace Booking.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Models;
    using Booking.Services.Data;
    using Booking.Web.InputModels.SearchProperties;
    using Booking.Web.ViewModels.Bookings;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class BookingsController : BaseController
    {
        private readonly IBookingsService bookingsService;
        private readonly UserManager<ApplicationUser> userManager;

        public BookingsController(
            IBookingsService bookingsService,
            UserManager<ApplicationUser> userManager)
        {
            this.bookingsService = bookingsService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> All()
        {
            var viewModel = new BookingsListViewModel();

            var user = await this.userManager.GetUserAsync(this.User);
            viewModel.Bookings = this.bookingsService.GetAllByUserId(user.Id);
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Book(BookingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData[GlobalConstants.ErrorMessages.BookingErrorKey] = GlobalConstants.ErrorMessages.BookingErrorValue;
                return this.RedirectToAction(
                    "ById",
                    "SearchProperties",
                    new SearchedInputModel
                    {
                        CheckIn = input.CheckIn,
                        CheckOut = input.CheckOut,
                        Id = input.PropertyId,
                        Members = input.Members,
                    });
            }

            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.bookingsService.AddAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.TempData[GlobalConstants.ErrorMessages.BookingErrorKey] = ex.Message;
                return this.RedirectToAction(
                    "ById",
                    "SearchProperties",
                    new SearchedInputModel
                    {
                        CheckIn = input.CheckIn,
                        CheckOut = input.CheckOut,
                        Id = input.PropertyId,
                        Members = input.Members,
                    });
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Cancel(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.bookingsService.DeleteAsync(id, user.Id);
            }
            catch (Exception ex)
            {
                this.TempData[GlobalConstants.ErrorMessages.BookingErrorKey] = ex.Message;
                return this.RedirectToAction(nameof(this.All));
            }

            this.TempData[GlobalConstants.SuccessMessages.BookingKey] = GlobalConstants.SuccessMessages.BookingValue;
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
