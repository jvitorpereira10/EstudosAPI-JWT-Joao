using Microsoft.EntityFrameworkCore;
using Ok.Tech.Domain.Entities;
using Ok.Tech.Domain.Repositories;
using Ok.Tech.Infra.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ok.Tech.Infra.Data.Repositories
{
  public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
  {
    protected readonly DbSet<TEntity> _entities;
    public Repository(ApiDbContext context)
    {
      _entities = context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
      return await _entities.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(Guid id)
    {
      return await _entities.FindAsync(id);
    }

    public void Create(TEntity entity)
    {
      _entities.Add(entity);
    }

    public void Update(TEntity entity)
    {
      _entities.Update(entity);
    }

    public void Delete(TEntity entity)
    {
      _entities.Remove(entity);
    }
  }
}
