namespace Booking.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Services.Data;
    using Booking.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    public class OffersController : BaseController
    {
        private readonly IFacilitiesService facilitiesService;
        private readonly IBedTypesService bedTypesService;
        private readonly IOffersService offersService;
        private readonly IPropertiesService propertiesService;
        private readonly ICurrenciesService currenciesService;
        private readonly IWebHostEnvironment environment;

        public OffersController(
            IFacilitiesService facilitiesService,
            IBedTypesService bedTypesService,
            IOffersService offersService,
            IPropertiesService propertiesService,
            ICurrenciesService currenciesService,
            IWebHostEnvironment environment)
        {
            this.facilitiesService = facilitiesService;
            this.bedTypesService = bedTypesService;
            this.offersService = offersService;
            this.propertiesService = propertiesService;
            this.currenciesService = currenciesService;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Add(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var viewModel = new AddOfferInputModel();
            viewModel.OfferFacilities = this.facilitiesService.GetAllFacilitiesExceptGeneral();
            viewModel.BedTypes = this.bedTypesService.GetAllBedTypes();
            viewModel.CurrencyCode = this.currenciesService.GetCurrencyByPropertyId(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(string id, AddOfferInputModel input)
        {
            if (input.BedTypesCounts.Count() != 4
                || input.BedTypesCounts.All(b => b <= 0)
                || input.BedTypesCounts.Any(b => b < 0))
            {
                this.ModelState.AddModelError(nameof(input.BedTypesCounts), "Enter correct sleeping places. At least one is required.");
            }

            this.ValidateCheckToDate(input);

            if (!this.ModelState.IsValid)
            {
                input.OfferFacilities = this.facilitiesService.GetAllFacilitiesExceptGeneral();
                input.BedTypes = this.bedTypesService.GetAllBedTypes();
                input.CurrencyCode = this.currenciesService.GetCurrencyByPropertyId(id);

                return this.View(input);
            }

            try
            {
                await this.offersService.AddOfferToProperty(id, input, $"{this.environment.WebRootPath}/images/offers/");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(nameof(input.Images), ex.Message);

                input.OfferFacilities = this.facilitiesService.GetAllFacilitiesExceptGeneral();
                input.BedTypes = this.bedTypesService.GetAllBedTypes();
                input.CurrencyCode = this.currenciesService.GetCurrencyByPropertyId(id);

                return this.View(input);
            }

            return this.RedirectToAction("ById", "Properties", new { id = id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var propertyId = this.propertiesService.GetPropertyIdByOfferId(id);
            await this.offersService.DeleteAsync(id);

            return this.RedirectToAction("ById", "Properties", new { id = propertyId });
        }

        public IActionResult Edit(string id)
        {
            var viewModel = this.offersService.GetById(id);
            var propertyId = this.propertiesService.GetPropertyIdByOfferId(id);
            viewModel.CurrencyCode = this.currenciesService.GetCurrencyByPropertyId(propertyId);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditOfferViewModel input)
        {
            this.ValidateCheckToDate(input);
            var propertyId = this.propertiesService.GetPropertyIdByOfferId(id);
            if (!this.ModelState.IsValid)
            {
                input.CurrencyCode = this.currenciesService.GetCurrencyByPropertyId(propertyId);

                return this.View(input);
            }

            await this.offersService.UpdateAsync(id, input);

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
