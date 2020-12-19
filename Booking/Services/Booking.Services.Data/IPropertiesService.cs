namespace Booking.Services.Data
{
    using System.Threading.Tasks;

    using Booking.Web.ViewModels.Home;
    using Booking.Web.ViewModels.PropertiesViewModels;
    using Booking.Web.ViewModels.SearchProperties;

    public interface IPropertiesService
    {
        string GetNameById(string id);

        Task CreateAsync(AddPropertyInputModel input, string userId, string imagePath);

        bool CheckIfNameIsAvailable(string name);

        bool CheckIfEditInputNameIsAvailable(string name, string propertyId);

        string GetIdByName(string propertyName);

        PropertiesListViewModel GetAllByUserId(string userId);

        EditPropertyInputModel GetById(string propertyId, string userId);

        Task UpdateAsync(EditPropertyInputModel input, string userId);

        Task DeleteAsync(string propertyId, string userId, string imagePath);

        PropertyByIdViewModel GetPropertyAndOffersById(string propertyId, string userId);

        string GetIdByOfferId(string id, string userId);

        SearchIndexInListViewModel GetBySearchRequirements(SearchIndexInputModel input);
    }
}
