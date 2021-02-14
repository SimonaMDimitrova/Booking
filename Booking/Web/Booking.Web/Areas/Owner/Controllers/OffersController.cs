namespace Booking.Web.Areas.Owner.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Models;
    using Booking.Services.Data;
    using Booking.Web.Controllers;
    using Booking.Web.InputModels.Offers;
    using Booking.Web.InputModels.Offers.Add;
    using Booking.Web.InputModels.Offers.Edit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area(GlobalConstants.OwnerRoleName)]
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
            var ifUserHasAccess = this.propertiesService.IsUserHasAccessToProperty(id, user.Id);
            if (!ifUserHasAccess)
            {
                this.TempData[GlobalConstants.ErrorMessages.PropertyAccessKey] = GlobalConstants.ErrorMessages.PropertyAccessValue;
                return this.RedirectToAction("All", "Properties");
            }

            var viewModel = new AddOfferInputModel
            {
                PropertyId = id,
                PropertyName = this.propertiesService.GetNameById(id),
                OfferFacilities = this.facilitiesService.GetAllExeptInGeneralCategory(),
                BedTypes = this.bedTypesService.GetAllByKeyValuePairs(),
                CurrencyCode = this.currenciesService.GetByPropertyId(id),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddOfferInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var ifUserHasAccess = this.propertiesService.IsUserHasAccessToProperty(input.PropertyId, user.Id);
            if (!ifUserHasAccess)
            {
                this.TempData[GlobalConstants.ErrorMessages.PropertyAccessKey] = GlobalConstants.ErrorMessages.PropertyAccessValue;
                return this.RedirectToAction("All", "Properties");
            }

            this.ValidateCheckToDate(input);

            if (!this.ModelState.IsValid)
            {
                input.OfferFacilities = this.facilitiesService.GetAllExeptInGeneralCategory();
                input.BedTypes = this.bedTypesService.GetAllByKeyValuePairs();
                input.CurrencyCode = this.currenciesService.GetByPropertyId(input.PropertyId);
                input.PropertyName = this.propertiesService.GetNameById(input.PropertyId);

                return this.View(input);
            }

            try
            {
                await this.offersService.CreateAsync(input, $"{this.environment.WebRootPath}{GlobalConstants.OfferImagesPath}");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains(GlobalConstants.ErrorMessages.MembersCount))
                {
                    this.ModelState.AddModelError(nameof(input.BedTypesCounts), ex.Message);
                }
                else
                {
                    this.ModelState.AddModelError(nameof(input.Images), ex.Message);
                }

                input.OfferFacilities = this.facilitiesService.GetAllExeptInGeneralCategory();
                input.BedTypes = this.bedTypesService.GetAllByKeyValuePairs();
                input.CurrencyCode = this.currenciesService.GetByPropertyId(input.PropertyId);

                return this.View(input);
            }

            this.TempData[GlobalConstants.SuccessMessages.AddOfferKey] = GlobalConstants.SuccessMessages.AddOfferValue;
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
                this.TempData[GlobalConstants.ErrorMessages.PropertyAccessKey] = ex.Message;
                return this.RedirectToAction("All", "Properties");
            }

            try
            {
                await this.offersService.DeleteAsync(id, user.Id, $"{this.environment.WebRootPath}{GlobalConstants.OfferImagesPath}");
            }
            catch (Exception ex)
            {
                this.TempData[GlobalConstants.ErrorMessages.OfferAccessKey] = ex.Message;
                return this.RedirectToAction("ById", "Properties", new { id = propertyId });
            }

            this.TempData[GlobalConstants.SuccessMessages.DeleteOfferKey] = GlobalConstants.SuccessMessages.DeleteOfferValue;
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
                this.TempData[GlobalConstants.ErrorMessages.PropertyAccessKey] = ex.Message;
                return this.RedirectToAction("All", "Properties");
            }

            EditOfferViewModel viewModel = this.offersService.GetById(id, user.Id);

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
                this.TempData[GlobalConstants.ErrorMessages.PropertyAccessKey] = ex.Message;
                return this.RedirectToAction("All", "Properties");
            }

            this.ValidateCheckToDate(input);

            if (!this.ModelState.IsValid)
            {
                input.PropertyId = propertyId;
                input.PropertyName = this.propertiesService.GetNameById(propertyId);
                input.CurrencyCode = this.currenciesService.GetByPropertyId(propertyId);

                return this.View(input);
            }

            await this.offersService.UpdateAsync(input);

            this.TempData[GlobalConstants.SuccessMessages.EditOfferKey] = GlobalConstants.SuccessMessages.EditOfferValue;
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
                    this.ModelState.AddModelError(nameof(input.ValidTo), GlobalConstants.ErrorMessages.OfferValidTo);
                }
            }
        }
    }
}
