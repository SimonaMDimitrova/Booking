namespace Booking.Web.ViewModels.SearchProperties
{
    using Booking.Web.ViewModels.Offers;

    public class SearchedOfferViewModel : OfferBaseViewModel
    {
        public string CheckIn { get; set; }

        public string CheckOut { get; set; }
    }
}
