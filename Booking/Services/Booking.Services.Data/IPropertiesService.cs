namespace Booking.Services.Data
{
    using System.Threading.Tasks;

    using Booking.Web.ViewModels.Home;
    using Booking.Web.ViewModels.PropertiesViewModels.Add;
    using Booking.Web.ViewModels.PropertiesViewModels.All;
    using Booking.Web.ViewModels.PropertiesViewModels.ById;
    using Booking.Web.ViewModels.PropertiesViewModels.Edit;
    using Booking.Web.ViewModels.SearchProperties;
    using Booking.Web.ViewModels.ViewComponents.SearchResults;

    public interface IPropertiesService
    {
        // done
        PropertiesListViewModel GetAllByUserId(string userId);

        string GetNameById(string id);

        // done
        Task CreateAsync(AddPropertyInputModel input, string userId, string imagePath);

        // done
        bool CheckIfNameIsAvailable(string name);

        // done
        bool CheckIfEditInputNameIsAvailable(string name, string propertyId);

        string GetIdByName(string propertyName);

        // done
        EditPropertyInputModel GetById(string propertyId, string userId);

        // done
        Task UpdateAsync(EditPropertyInputModel input);

        // done
        Task DeleteAsync(string propertyId, string userId, string imagePath);

        // done
        PropertyByIdViewModel GetPropertyAndOffersById(string propertyId, string userId);

        string GetIdByOfferId(string id, string userId);

        SearchIndexListViewModel GetBySearchRequirements(IndexInputModel input, string userEmail);

        SearchedPropertyByIdViewModel GetByIdBasedOnSearchRequirements(SearchedInputModel input);
    }
}
