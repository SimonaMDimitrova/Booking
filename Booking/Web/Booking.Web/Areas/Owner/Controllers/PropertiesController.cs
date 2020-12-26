namespace Booking.Web.Areas.Owner.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Models;
    using Booking.Services.Data;
    using Booking.Web.Controllers;
    using Booking.Web.ViewModels.PropertiesViewModels.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.All;
    using Booking.Web.ViewModels.PropertiesViewModels.Edit;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area(GlobalConstants.OwnerRoleName)]
    public class PropertiesController : PropertiesBaseController
    {
        private readonly ICountriesService countriesService;
        private readonly IPropertyCategoriesService propertyCategoriesService;
        private readonly IPropertiesService propertiesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFacilitiesService facilitiesService;
        private readonly IRulesService rulesService;
        private readonly IWebHostEnvironment environment;
        private readonly IOffersService offersService;

        public PropertiesController(
            ICountriesService countriesService,
            ITownsService townsService,
            ICurrenciesService currenciesService,
            IPropertyCategoriesService propertyCategoriesService,
            IPropertiesService propertiesService,
            UserManager<ApplicationUser> userManager,
            IFacilitiesService facilitiesService,
            IRulesService rulesService,
            IWebHostEnvironment environment,
            IOffersService offersService)
            : base(townsService, currenciesService)
        {
            this.countriesService = countriesService;
            this.propertyCategoriesService = propertyCategoriesService;
            this.propertiesService = propertiesService;
            this.userManager = userManager;
            this.facilitiesService = facilitiesService;
            this.rulesService = rulesService;
            this.environment = environment;
            this.offersService = offersService;
        }

        public async Task<IActionResult> All()
        {
            PropertiesListViewModel viewModel;
            var user = await this.userManager.GetUserAsync(this.User);
            viewModel = this.propertiesService.GetAllByUserId(user.Id);

            return this.View(viewModel);
        }

        public IActionResult Add()
        {
            var viewModel = new AddPropertyInputModel();
            viewModel.Countries = this.countriesService.GetAllByKeyValuePairs();
            viewModel.PropertyCategories = this.propertyCategoriesService.GetAllByKeyValuePairs();
            viewModel.Facilities = this.facilitiesService.GetAllInGeneralCategory();
            viewModel.Rules = this.rulesService.GetAll();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPropertyInputModel input)
        {
            if (this.propertiesService.CheckIfNameIsAvailable(input.Name))
            {
                this.ModelState.AddModelError(nameof(input.Name), GlobalConstants.ErrorMessages.PropertyNameIsAlreadyUsed);
            }

            if (!this.ModelState.IsValid)
            {
                input.Countries = this.countriesService.GetAllByKeyValuePairs();
                input.PropertyCategories = this.propertyCategoriesService.GetAllByKeyValuePairs();
                input.Facilities = this.facilitiesService.GetAllInGeneralCategory();
                input.Rules = this.rulesService.GetAll();

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.propertiesService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}{GlobalConstants.PropertyImagesPath}");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(nameof(input.Images), ex.Message);

                input.Countries = this.countriesService.GetAllByKeyValuePairs();
                input.PropertyCategories = this.propertyCategoriesService.GetAllByKeyValuePairs();
                input.Facilities = this.facilitiesService.GetAllInGeneralCategory();
                input.Rules = this.rulesService.GetAll();

                return this.View(input);
            }

            this.TempData[GlobalConstants.SuccessMessages.AddKey] = GlobalConstants.SuccessMessages.AddValue;
            return this.Redirect(nameof(this.All));
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.propertiesService.GetById(id, user.Id);
            if (viewModel == null)
            {
                this.TempData[GlobalConstants.ErrorMessages.EditErrorKey] = GlobalConstants.ErrorMessages.EditErrorValue;
                return this.RedirectToAction(nameof(this.All));
            }

            viewModel.Facilities = this.facilitiesService.GetAllByPropertyId(id);
            viewModel.Rules = this.rulesService.GetAllByPropertyId(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPropertyInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var property = this.propertiesService.GetById(input.Id, user.Id);
            if (property == null)
            {
                this.TempData[GlobalConstants.ErrorMessages.EditErrorKey] = GlobalConstants.ErrorMessages.EditErrorValue;
                return this.RedirectToAction(nameof(this.All));
            }

            if (this.propertiesService.CheckIfEditInputNameIsAvailable(input.Name, input.Id))
            {
                this.ModelState.AddModelError(nameof(input.Name), GlobalConstants.ErrorMessages.PropertyNameIsAlreadyUsed);
            }

            if (!this.ModelState.IsValid)
            {
                input.Rules = this.rulesService.GetAllByPropertyId(input.Id);
                input.Facilities = this.facilitiesService.GetAllByPropertyId(input.Id);

                return this.View(input);
            }

            await this.propertiesService.UpdateAsync(input);

            this.TempData[GlobalConstants.SuccessMessages.EditKey] = GlobalConstants.SuccessMessages.EditValue;
            return this.RedirectToAction(nameof(this.ById), new { id = input.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.offersService.DeleteAllByPropertyIdAsync(id, user.Id, $"{this.environment.WebRootPath}{GlobalConstants.OfferImagesPath}");
            }
            catch (Exception)
            {
                this.TempData[GlobalConstants.ErrorMessages.DeleteErrorKey] = GlobalConstants.ErrorMessages.DeleteErrorValue;
                return this.RedirectToAction(nameof(this.All));
            }

            try
            {
                await this.propertiesService.DeleteAsync(id, user.Id, $"{this.environment.WebRootPath}{GlobalConstants.PropertyImagesPath}");
            }
            catch (Exception ex)
            {
                this.TempData[GlobalConstants.ErrorMessages.DeleteErrorKey] = ex.Message;
                return this.RedirectToAction(nameof(this.All));
            }

            this.TempData[GlobalConstants.SuccessMessages.DeleteKey] = GlobalConstants.SuccessMessages.DeleteValue;
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> ById(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.propertiesService.GetPropertyAndOffersById(id, user.Id);
            if (viewModel == null)
            {
                this.TempData[GlobalConstants.ErrorMessages.ByIdErrorKey] = GlobalConstants.ErrorMessages.ByIdErrorValue;
                return this.RedirectToAction(nameof(this.All));
            }

            return this.View(viewModel);
        }
    }
}
