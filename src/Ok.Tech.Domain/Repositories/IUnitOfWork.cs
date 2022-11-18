using System.Threading.Tasks;

namespace Ok.Tech.Domain.Repositories
{
  public interface IUnitOfWork
  {
    public Task<int> SaveAsync();
  }
}