namespace Booking.Services.Data
{
    using System.Threading.Tasks;
    using Booking.Web.ViewModels.Home;
    using Booking.Web.ViewModels.PropertiesVM;
    using Booking.Web.ViewModels.SearchProperties;

    public interface IPropertiesService
    {
        Task CreateAsync(AddPropertyInputModel input, string userId);

        bool CheckIsPropertyNameAvailable(string name);

        bool CheckIfNewPropertyNameAvailable(string name, string propertyId);

        string GetPropertyIdByName(string propertyName);

        PropertyInListModel GetAllPropertiesByUserId(string userId);

        EditPropertyInputModel GetPropertyById(string propertyId, string userId);

        Task EditProperty(EditPropertyInputModel input);

        Task DeleteAsync(string propertyId);

        PropertyByIdViewModel GetPropertyAndOffersById(string propertyId, string userId);

        string GetPropertyIdByOfferId(string id);

        SearchIndexInListViewModel GetBySearchRequirements(SearchIndexInputModel input);
    }
}
