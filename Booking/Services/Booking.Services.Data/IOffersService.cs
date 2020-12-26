namespace Booking.Services.Data
{
    using System.Threading.Tasks;

    using Booking.Web.ViewModels.Offers.Add;
    using Booking.Web.ViewModels.Offers.Edit;

    public interface IOffersService
    {
        Task DeleteAllByPropertyIdAsync(string propertyId, string userId, string imagePath);

        Task CreateAsync(AddOfferInputModel input, string imagePath);

        Task DeleteAsync(string offerId, string userId, string imagePath);

        Task UpdateAsync(EditOfferViewModel input);

        EditOfferViewModel GetById(string id, string userId);
    }
}
