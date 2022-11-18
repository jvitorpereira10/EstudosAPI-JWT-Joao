using Ok.Tech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ok.Tech.Domain.Repositories
{
  public interface IRepository<TEntity> where TEntity : Entity
  {
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
  }
}
