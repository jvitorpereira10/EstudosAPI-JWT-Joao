using Microsoft.EntityFrameworkCore;
using Ok.Tech.Domain.Entities;
using Ok.Tech.Domain.Repositories;
using Ok.Tech.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ok.Tech.Infra.Data.Repositories
{
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
        public PriceRepository(ApiDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Price>> GetAllWithPriceListAndProductAsync()
        {
            return await _entities.Include(e => e.PriceList).Include(e => e.Product).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Price>> GetByProductIdWithPriceListAndProductAsync(Guid productId)
        {
            return await _entities
                .Where(p => p.ProductId == productId)
                .Include(e => e.PriceList)
                .Include(e => e.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Price>> GetByPriceListIdWithPriceListAndProductAsync(Guid priceListId)
        {
            return await _entities
                .Where(p => p.PriceListId == priceListId)
                .Include(e => e.PriceList)
                .Include(e => e.Product)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}