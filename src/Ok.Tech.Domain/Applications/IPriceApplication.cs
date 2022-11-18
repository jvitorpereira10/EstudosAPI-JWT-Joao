using Ok.Tech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ok.Tech.Domain.Applications
{
    public interface IPriceApplication : IApplication<Price>
    {
        Task<IEnumerable<Price>> GetAllWithPriceListAndProductAsync();

        Task<IEnumerable<Price>> GetByProductIdWithPriceListAndProductAsync(Guid productId);

        Task<IEnumerable<Price>> GetByPriceListIdWithPriceListAndProductAsync(Guid priceListId);
    }
}