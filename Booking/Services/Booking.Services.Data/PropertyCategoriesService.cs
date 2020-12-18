namespace Booking.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Booking.Data.Common.Repositories;
    using Booking.Data.Models;

    public class PropertyCategoriesService : IPropertyCategoriesService
    {
        private readonly IRepository<PropertyCategory> propertyCategoriesRepositorty;

        public PropertyCategoriesService(IRepository<PropertyCategory> propertyCategoriesRepositorty)
        {
            this.propertyCategoriesRepositorty = propertyCategoriesRepositorty;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllByKeyValuePairs()
        {
            return this.propertyCategoriesRepositorty.All()
                .Select(pc => new
                {
                    pc.Id,
                    pc.Name,
                })
                .OrderBy(c => c.Name)
                .ToList().Select(c => new KeyValuePair<string, string>(c.Id.ToString(), c.Name));
        }
    }
}
