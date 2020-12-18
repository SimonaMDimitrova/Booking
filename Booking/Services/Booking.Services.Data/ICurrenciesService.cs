namespace Booking.Services.Data
{
    public interface ICurrenciesService
    {
        string GetByCountryId(int id);

        string GetByPropertyId(string id);
    }
}
