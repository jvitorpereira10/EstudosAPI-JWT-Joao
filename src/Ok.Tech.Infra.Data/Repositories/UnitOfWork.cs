using Ok.Tech.Domain.Repositories;
using Ok.Tech.Infra.Data.Contexts;
using System.Threading.Tasks;

namespace Ok.Tech.Infra.Data.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ApiDbContext _context;

    public UnitOfWork(ApiDbContext context)
    {
      _context = context;
    }

    public async Task<int> SaveAsync()
    {
      return await _context.SaveChangesAsync();
    }
  }
}
