using Ok.Tech.Domain.Entities;
using Ok.Tech.Domain.Repositories;
using Ok.Tech.Infra.Data.Contexts;

namespace Ok.Tech.Infra.Data.Repositories
{
  public class PriceListRepository : Repository<PriceList>, IPriceListRepository
  {
    public PriceListRepository(ApiDbContext context) : base(context)
    {
    }
  }
}
