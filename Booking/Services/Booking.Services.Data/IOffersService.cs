namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Booking.Web.ViewModels.Bookings;
    using Booking.Web.ViewModels.Offers;

    public interface IOffersService
    {
        Task DeleteAllByPropertyIdAsync(string id, string userId, string imagePath);

        Task AddToProperty(AddOfferInputModel input, string imagePath);

        Task DeleteAsync(string offerId, string userId, string imagePath);

        Task UpdateAsync(string userId, EditOfferViewModel input);

        EditOfferViewModel GetById(string id, string userId);
    }
}
