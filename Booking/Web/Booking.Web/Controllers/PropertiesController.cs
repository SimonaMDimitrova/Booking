namespace Booking.Web.Controllers
{
    using System.Threading.Tasks;

    using Booking.Data.Models;
    using Booking.Services.Data;
    using Booking.Web.ViewModels.Properties;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class PropertiesController : PropertiesBaseController
    {
        private readonly ICountriesService countriesService;
        private readonly IPropertyCategoriesService propertyCategoriesService;
        private readonly IPropertiesService propertiesService;
        private readonly UserManager<ApplicationUser> userManager;

        public PropertiesController(
            ICountriesService countriesService,
            ITownsService townsService,
            ICurrenciesService currenciesService,
            IPropertyCategoriesService propertyCategoriesService,
            IPropertiesService propertiesService,
            UserManager<ApplicationUser> userManager)
            : base(townsService, currenciesService)
        {
            this.countriesService = countriesService;
            this.propertyCategoriesService = propertyCategoriesService;
            this.propertiesService = propertiesService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult Add()
        {
            var viewModel = new AddPropertyInputModel();
            viewModel.Countries = this.countriesService.GetAllByKeyValuePairs();
            viewModel.PropertyCategories = this.propertyCategoriesService.GetAllByKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddPropertyInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.Countries = this.countriesService.GetAllByKeyValuePairs();
                input.PropertyCategories = this.propertyCategoriesService.GetAllByKeyValuePairs();

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.propertiesService.CreateAsync(input, user.Id);
            return this.Redirect(nameof(this.All));
        }
    }
}
