using Ok.Tech.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ok.Tech.Domain.Applications
{
  public interface IApplication<TEntity> where TEntity : Entity
  {
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(Guid id);
    Task Create(TEntity entity);
    Task Update(Guid id, TEntity entity);
    Task Delete(Guid id);
  }
}