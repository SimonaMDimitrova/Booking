namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Booking.Web.ViewModels.PropertiesVM;

    public interface IPropertiesService
    {
        Task CreateAsync(AddPropertyInputModel input, string userId);

        bool CheckIsPropertyNameAvailable(string name);

        string GetPropertyIdByName(string propertyName);

        PropertyInListModel GetAllPropertiesByUserId(string userId);

        EditPropertyInputModel GetPropertyById(string propertyId, string userId);

        Task EditProperty(EditPropertyInputModel input);

        Task DeleteAsync(string propertyId);
    }
}
