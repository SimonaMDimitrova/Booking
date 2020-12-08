namespace Booking.Services.Data
{
    using System.Threading.Tasks;

    using Booking.Web.ViewModels.Properties;

    public interface IPropertiesService
    {
        Task CreateAsync(AddPropertyInputModel input, string userId);
    }
}
