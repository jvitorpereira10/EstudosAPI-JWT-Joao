using Ok.Tech.Domain.Entities;
using Ok.Tech.Domain.Repositories;
using Ok.Tech.Infra.Data.Contexts;

namespace Ok.Tech.Infra.Data.Repositories
{
  public class ProductRepository : Repository<Product>, IProductRepository
  {
    public ProductRepository(ApiDbContext context) : base(context)
    {

    }
  }
}
