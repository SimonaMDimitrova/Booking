namespace Booking.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Booking.Data.Models;
    using Booking.Services.Data;
    using Booking.Web.ViewModels.Facilities;
    using Booking.Web.ViewModels.PropertiesVM;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PropertiesController : PropertiesBaseController
    {
        private readonly ICountriesService countriesService;
        private readonly IPropertyCategoriesService propertyCategoriesService;
        private readonly IPropertiesService propertiesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IFacilitiesService facilitiesService;
        private readonly IRulesService rulesService;

        public PropertiesController(
            ICountriesService countriesService,
            ITownsService townsService,
            ICurrenciesService currenciesService,
            IPropertyCategoriesService propertyCategoriesService,
            IPropertiesService propertiesService,
            UserManager<ApplicationUser> userManager,
            IFacilitiesService facilitiesService,
            IRulesService rulesService)
            : base(townsService, currenciesService)
        {
            this.countriesService = countriesService;
            this.propertyCategoriesService = propertyCategoriesService;
            this.propertiesService = propertiesService;
            this.userManager = userManager;
            this.facilitiesService = facilitiesService;
            this.rulesService = rulesService;
        }

        public async Task<IActionResult> All()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.propertiesService.GetAllPropertiesByUserId(user.Id);

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Add()
        {
            var viewModel = new AddPropertyInputModel();
            viewModel.Countries = this.countriesService.GetAllByKeyValuePairs();
            viewModel.PropertyCategories = this.propertyCategoriesService.GetAllByKeyValuePairs();
            viewModel.Facilities = this.facilitiesService.GetAllGeneralFacilities();
            viewModel.Rules = this.rulesService.GetAllRules();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddPropertyInputModel input)
        {
            if (this.propertiesService.CheckIsPropertyNameAvailable(input.Name))
            {
                this.ModelState.AddModelError(nameof(input.Name), "This property name is already used. Try different one!");
            }

            if (!this.ModelState.IsValid)
            {
                input.Countries = this.countriesService.GetAllByKeyValuePairs();
                input.PropertyCategories = this.propertyCategoriesService.GetAllByKeyValuePairs();
                input.Facilities = this.facilitiesService.GetAllGeneralFacilities();
                input.Rules = this.rulesService.GetAllRules();

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.propertiesService.CreateAsync(input, user.Id);

            return this.Redirect(nameof(this.All));
        }

        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.propertiesService.GetPropertyById(id, user.Id);
            viewModel.Facilities = this.facilitiesService.GetAllFacilitiesByPropertyId(id);
            viewModel.Rules = this.rulesService.GetAllRulesByPropertyId(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditPropertyInputModel input)
        {
            if (this.propertiesService.CheckIfNewPropertyNameAvailable(input.Name, input.Id))
            {
                this.ModelState.AddModelError(nameof(input.Name), "This property name is already used. Try different one!");
            }

            if (!this.ModelState.IsValid)
            {
                input.Rules = this.rulesService.GetAllRulesByPropertyId(input.Id);
                input.Facilities = this.facilitiesService.GetAllFacilitiesByPropertyId(input.Id);

                return this.View(input);
            }

            if (input.Id == null)
            {
                return this.NotFound();
            }

            await this.propertiesService.EditProperty(input);

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await this.propertiesService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }

        public async Task<IActionResult> ById(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var user = await this.userManager.GetUserAsync(this.User);
            var viewModel = this.propertiesService.GetPropertyAndOffersById(id, user.Id);

            return this.View(viewModel);
        }
    }
}
