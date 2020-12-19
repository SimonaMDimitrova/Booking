namespace Booking.Web.Areas.Owner.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Booking.Common;
    using Booking.Data.Models;
    using Booking.Services.Data;
    using Booking.Web.Controllers;
    using Booking.Web.ViewModels.PropertiesViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area("Owner")]
    public class PropertiesController : PropertiesBaseController
    {
        private readonly ICountriesService countriesService;
        private readonly IPropertyCategoriesService propertyCategoriesService;
        private readonly IPropertiesService propertiesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFacilitiesService facilitiesService;
        private readonly IRulesService rulesService;
        private readonly IWebHostEnvironment environment;

        public PropertiesController(
            ICountriesService countriesService,
            ITownsService townsService,
            ICurrenciesService currenciesService,
            IPropertyCategoriesService propertyCategoriesService,
            IPropertiesService propertiesService,
            UserManager<ApplicationUser> userManager,
            IFacilitiesService facilitiesService,
            IRulesService rulesService,
            IWebHostEnvironment environment)
            : base(townsService, currenciesService)
        {
            this.countriesService = countriesService;
            this.propertyCategoriesService = propertyCategoriesService;
            this.propertiesService = propertiesService;
            this.userManager = userManager;
            this.facilitiesService = facilitiesService;
            this.rulesService = rulesService;
            this.environment = environment;
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
                this.ModelState.AddModelError(nameof(input.Name), "This property name is already used. Try different one!");
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
                await this.propertiesService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images/properties/");
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

            this.TempData["Message"] = "Property was successfully created.";
            return this.Redirect(nameof(this.All));
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                this.TempData["Error"] = "The property is invalid.";
                return this.RedirectToAction(nameof(this.All));
            }

            var user = await this.userManager.GetUserAsync(this.User);
            EditPropertyInputModel viewModel;
            try
            {
                viewModel = this.propertiesService.GetById(id, user.Id);
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                return this.RedirectToAction(nameof(this.ById), new { id = id });
            }

            viewModel.Facilities = this.facilitiesService.GetAllByPropertyId(id);
            viewModel.Rules = this.rulesService.GetAllByPropertyId(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPropertyInputModel input)
        {
            if (this.propertiesService.CheckIfEditInputNameIsAvailable(input.Name, input.Id))
            {
                this.ModelState.AddModelError(nameof(input.Name), "This property name is already used. Try different one!");
            }

            if (!this.ModelState.IsValid)
            {
                input.Rules = this.rulesService.GetAllByPropertyId(input.Id);
                input.Facilities = this.facilitiesService.GetAllByPropertyId(input.Id);

                return this.View(input);
            }

            if (input.Id == null)
            {
                this.TempData["Error"] = "The property is invalid.";
                return this.RedirectToAction(nameof(this.All));
            }

            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.propertiesService.EditAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.TempData["Error"] = ex.Message;
                return this.RedirectToAction(nameof(this.All));
            }

            this.TempData["Message"] = "Property was successfully edited.";
            return this.RedirectToAction(nameof(this.ById), new { id = input.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.propertiesService.DeleteAsync(id, user.Id);
            }
            catch (Exception)
            {
                return this.RedirectToAction(nameof(this.All));
            }

            this.TempData["Message"] = "Property was successfully deleted.";
            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> ById(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.propertiesService.GetPropertyAndOffersById(id, user.Id);

            if (id == null || viewModel == null)
            {
                this.TempData["Error"] = "The property was not found.";
                return this.RedirectToAction(nameof(this.All));
            }

            return this.View(viewModel);
        }
    }
}
