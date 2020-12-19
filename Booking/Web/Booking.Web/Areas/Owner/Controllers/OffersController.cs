namespace Booking.Web.Areas.Owner.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Models;
    using Booking.Services.Data;
    using Booking.Web.Controllers;
    using Booking.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area("Owner")]
    public class OffersController : BaseController
    {
        private readonly IFacilitiesService facilitiesService;
        private readonly IBedTypesService bedTypesService;
        private readonly IOffersService offersService;
        private readonly IPropertiesService propertiesService;
        private readonly ICurrenciesService currenciesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public OffersController(
            IFacilitiesService facilitiesService,
            IBedTypesService bedTypesService,
            IOffersService offersService,
            IPropertiesService propertiesService,
            ICurrenciesService currenciesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.facilitiesService = facilitiesService;
            this.bedTypesService = bedTypesService;
            this.offersService = offersService;
            this.propertiesService = propertiesService;
            this.currenciesService = currenciesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public async Task<IActionResult> Add(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var property = this.propertiesService.GetById(id, user.Id);
            if (property == null)
            {
                this.TempData["Error"] = "You don't have permission to make any changes to this property (or it doesn't exists).";
                return this.RedirectToAction("All", "Properties");
            }

            var viewModel = new AddOfferInputModel();
            viewModel.PropertyId = id;
            viewModel.OfferFacilities = this.facilitiesService.GetAllExeptInGeneralCategory();
            viewModel.BedTypes = this.bedTypesService.GetAll();
            viewModel.CurrencyCode = this.currenciesService.GetByPropertyId(id);
            viewModel.PropertyName = this.propertiesService.GetNameById(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOfferInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var property = this.propertiesService.GetById(input.PropertyId, user.Id);
            if (property == null)
            {
                this.TempData["Error"] = "You don't have permission to make any changes to this property (or it doesn't exists).";
                return this.RedirectToAction("All", "Properties");
            }

            if (input.BedTypesCounts.Count() != 4
                || input.BedTypesCounts.All(b => b <= 0)
                || input.BedTypesCounts.Any(b => b < 0))
            {
                this.ModelState.AddModelError(nameof(input.BedTypesCounts), "Enter correct sleeping places. At least one is required.");
            }

            this.ValidateCheckToDate(input);

            if (!this.ModelState.IsValid)
            {
                input.OfferFacilities = this.facilitiesService.GetAllExeptInGeneralCategory();
                input.BedTypes = this.bedTypesService.GetAll();
                input.CurrencyCode = this.currenciesService.GetByPropertyId(input.PropertyId);
                input.PropertyName = this.propertiesService.GetNameById(input.PropertyId);

                this.TempData["Error"] = " ";

                return this.View(input);
            }

            try
            {
                await this.offersService.AddToProperty(input, $"{this.environment.WebRootPath}/images/offers/");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("offer"))
                {
                    this.ModelState.AddModelError(nameof(input.BedTypesCounts), ex.Message);
                }
                else
                {
                    this.ModelState.AddModelError(nameof(input.Images), ex.Message);
                }

                input.OfferFacilities = this.facilitiesService.GetAllExeptInGeneralCategory();
                input.BedTypes = this.bedTypesService.GetAll();
                input.CurrencyCode = this.currenciesService.GetByPropertyId(input.PropertyId);

                return this.View(input);
            }

            this.TempData["Message"] = "The offer was successfully added.";
            return this.RedirectToAction("ById", "Properties", new { id = input.PropertyId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string propertyId;
            try
            {
                propertyId = this.propertiesService.GetIdByOfferId(id, user.Id);
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                return this.RedirectToAction("All", "Properties");
            }

            try
            {
                await this.offersService.DeleteAsync(id, user.Id, $"{this.environment.WebRootPath}/images/offers/");
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                return this.RedirectToAction("ById", "Properties", new { id = propertyId });
            }

            this.TempData["Message"] = "The offer was successfully deleted.";
            return this.RedirectToAction("ById", "Properties", new { id = propertyId });
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string propertyId;
            try
            {
                propertyId = this.propertiesService.GetIdByOfferId(id, user.Id);
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                return this.RedirectToAction("All", "Properties");
            }

            EditOfferViewModel viewModel = this.offersService.GetById(id, user.Id);
            viewModel.CurrencyCode = this.currenciesService.GetByPropertyId(propertyId);
            viewModel.PropertyId = propertyId;
            viewModel.PropertyName = this.propertiesService.GetNameById(propertyId);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditOfferViewModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            string propertyId;
            try
            {
                propertyId = this.propertiesService.GetIdByOfferId(input.OfferId, user.Id);
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                return this.RedirectToAction("All", "Properties");
            }

            this.ValidateCheckToDate(input);

            if (!this.ModelState.IsValid)
            {
                input.CurrencyCode = this.currenciesService.GetByPropertyId(propertyId);

                return this.View(input);
            }

            await this.offersService.UpdateAsync(user.Id, input);

            this.TempData["Message"] = "The offer was successfully edited.";
            return this.RedirectToAction("ById", "Properties", new { id = propertyId });
        }

        private void ValidateCheckToDate(OfferBaseInputModel input)
        {
            if (input.ValidFrom != null && input.ValidTo != null)
            {
                var validFrom = (DateTime)input.ValidFrom;
                var validTo = (DateTime)input.ValidTo;

                if (!(validFrom.Date.AddDays(1) < validTo)
                    || validTo.Date.AddDays(1) < DateTime.UtcNow)
                {
                    this.ModelState.AddModelError(nameof(input.ValidTo), "The date must be at least 2 days after valid from date.");
                }
            }

            if (input.ValidFrom == null)
            {
                this.ModelState.AddModelError(nameof(input.ValidTo), "Fill valid from field first.");
            }
        }
    }
}
