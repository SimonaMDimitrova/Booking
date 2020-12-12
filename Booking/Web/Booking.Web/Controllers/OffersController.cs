namespace Booking.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Booking.Services.Data;
    using Booking.Web.ViewModels.Offers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class OffersController : BaseController
    {
        private readonly IFacilitiesService facilitiesService;
        private readonly IBedTypesService bedTypesService;
        private readonly IOffersService offersService;
        private readonly IPropertiesService propertiesService;

        public OffersController(
            IFacilitiesService facilitiesService,
            IBedTypesService bedTypesService,
            IOffersService offersService,
            IPropertiesService propertiesService)
        {
            this.facilitiesService = facilitiesService;
            this.bedTypesService = bedTypesService;
            this.offersService = offersService;
            this.propertiesService = propertiesService;
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

            if (input.ValidFrom != null && input.ValidTo != null)
            {
                var validFrom = (DateTime)input.ValidFrom;
                var validTo = (DateTime)input.ValidTo;

                if (!(validFrom.Date.AddDays(1) < validTo))
                {
                    this.ModelState.AddModelError(nameof(input.ValidTo), "The date must be at least 2 days after valid from date.");
                }
            }

            if (!this.ModelState.IsValid)
            {
                input.OfferFacilities = this.facilitiesService.GetAllFacilitiesExceptGeneral();
                input.BedTypes = this.bedTypesService.GetAllBedTypes();

                return this.View(input);
            }

            await this.offersService.AddOfferToProperty(id, input);

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
    }
}
