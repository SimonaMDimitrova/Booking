namespace Booking.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Booking.Web.ViewModels.Offers;

    public interface IOffersService
    {
        Task AddOfferToProperty(string propertyId, AddOfferInputModel input);

        Task DeleteAsync(string id);
    }
}
