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
        bool IsUserHasAccessToProperty(string propertyId, string userId);

        PropertiesListViewModel GetAllByUserId(string userId);

        string GetNameById(string id);

        Task CreateAsync(AddPropertyInputModel input, string userId, string imagePath);

        bool CheckIfNameIsAvailable(string name);

        bool CheckIfEditInputNameIsAvailable(string name, string propertyId);

        string GetIdByName(string propertyName);

        EditPropertyInputModel GetById(string propertyId, string userId);

        Task UpdateAsync(EditPropertyInputModel input);

        Task DeleteAsync(string propertyId, string userId, string imagePath);

        PropertyByIdViewModel GetPropertyAndOffersById(string propertyId, string userId);

        string GetIdByOfferId(string id, string userId);

        SearchIndexListViewModel GetBySearchRequirements(IndexInputModel input, string userEmail);

        SearchedPropertyByIdViewModel GetByIdBasedOnSearchRequirements(SearchedInputModel input);
    }
}
